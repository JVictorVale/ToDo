using AutoMapper;
using Microsoft.AspNetCore.Http;
using ToDo.Application.Contracts;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.DTOs.ViewModel;
using ToDo.Application.Extensions;
using ToDo.Application.Notification;
using ToDo.Domain.Contracts.Repositories;
using ToDo.Domain.Filter;
using ToDo.Domain.Models;

namespace ToDo.Application.Services;

public class AssignmentService : BaseService, IAssignmentService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IAssignmentListRepository _assignmentListRepository;
    
    public AssignmentService(IMapper mapper, INotificator notificator, IHttpContextAccessor httpContextAccessor, IAssignmentRepository assignmentRepository, IAssignmentListRepository assignmentListRepository) : base(mapper, notificator)
    {
        _httpContextAccessor = httpContextAccessor;
        _assignmentRepository = assignmentRepository;
        _assignmentListRepository = assignmentListRepository;
    }

    public async Task<PagedDto<AssignmentDto>> SearchAsync(AssignmentSearchDto dto)
    {
        var filter = Mapper.Map<AssignmentFilter>(dto);
        
        var result = await _assignmentRepository.SearchAsync(_httpContextAccessor.GetUserId(), filter, dto.PerPage,
            dto.Page);

        return new PagedDto<AssignmentDto>
        {
            List = Mapper.Map<List<AssignmentDto>>(result.Items),
            Total = result.Total,
            Page = result.Page,
            PerPage = result.PerPage,
            PageCount = result.PageCount
        };
    }

    public async Task<AssignmentDto?> GetByIdAsync(int id)
    {
        var getAssignment =
            await _assignmentRepository.GetByIdAsync(id, _httpContextAccessor.GetUserId());

        if (getAssignment != null) return Mapper.Map<AssignmentDto>(getAssignment);

        Notificator.HandleNotFoundResource();
        return null;
    }

    public async Task<AssignmentDto?> CreateAsync(AddAssignmentDto dto)
    {
        var assignment = Mapper.Map<Assignment>(dto);
        assignment.UserId = _httpContextAccessor.GetUserId() ?? 0;

        if (!await Validate(assignment)) return null;

        _assignmentRepository.CreateAsync(assignment);

        if (await _assignmentRepository.UnityOfWork.Commit())
            return Mapper.Map<AssignmentDto>(assignment);

        Notificator.Handle("Não foi possível cadastrar a tarefa");
        return null;
    }

    public async Task<AssignmentDto?> UpdateAsync(int id, UpdateAssignmentDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("Os ids não conferem");
            return null;
        }

        var getAssignment = await _assignmentRepository.GetByIdAsync(id, _httpContextAccessor.GetUserId());

        if (getAssignment == null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }

        var result = Mapper.Map(dto, getAssignment);

        if (!await Validate(result)) return null;

        _assignmentRepository.UpdateAsync(getAssignment);

        if (await _assignmentRepository.UnityOfWork.Commit())
            return Mapper.Map<AssignmentDto>(result);

        Notificator.Handle("Não foi possível atualizar a tarefa");
        return null;
    }

    public async Task DeleteAsync(int id)
    {
        var getAssignment = await _assignmentRepository.GetByIdAsync(id, _httpContextAccessor.GetUserId());

        if (getAssignment == null)
        {
            Notificator.HandleNotFoundResource();
            return;
        }

        _assignmentRepository.DeleteAsync(getAssignment);

        if (!await _assignmentRepository.UnityOfWork.Commit())
        {
            Notificator.Handle("Não foi possível remover a tarefa");
        }
    }

    public async Task MarkConcludedAsync(int id)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id, _httpContextAccessor.GetUserId());

        if (assignment == null)
        {
            Notificator.HandleNotFoundResource();
            return;
        }

        assignment.SetConcluded();

        _assignmentRepository.UpdateAsync(assignment);

        if (!await _assignmentRepository.UnityOfWork.Commit())
        {
            Notificator.Handle("Não foi possível marcar a tarefa como concluída");
        }
    }

    public async Task MarkDesconcludedAsync(int id)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id, _httpContextAccessor.GetUserId());

        if (assignment == null)
        {
            Notificator.HandleNotFoundResource();
            return;
        }

        assignment.SetUnconcluded();

        _assignmentRepository.UpdateAsync(assignment);

        if (!await _assignmentRepository.UnityOfWork.Commit())
        {
            Notificator.Handle("Não foi possível marcar a tarefa como não concluída");
        }
    }
    
    private async Task<bool> Validate(Assignment assignment)
    {
        if(!assignment.Validate(out var validationResult)) 
            Notificator.Handle(validationResult.Errors);
        
        var existingAssignmentList =
            await _assignmentListRepository.FirstOrDefaultAsync(u => u.Id == assignment.AssignmentListId);

        if (existingAssignmentList == null)
        {
            Notificator.Handle("Não existe essa lista de tarefa.");
        }
        
        var existingAssignment =
            await _assignmentRepository.FirstOrDefaultAsync(u =>
                u.Description == assignment.Description && u.AssignmentListId == assignment.AssignmentListId);

        if (existingAssignment != null)
        {
            Notificator.Handle("Já existe uma tarefa cadastrada com essas informações.");
        }

        return !Notificator.HasNotification;
    }
}