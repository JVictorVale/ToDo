using AutoMapper;
using Microsoft.AspNetCore.Http;
using ToDo.Application.Contracts;
using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.DTOs.ViewModel;
using ToDo.Application.Extensions;
using ToDo.Application.Notification;
using ToDo.Domain.Contracts.Interfaces;
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

    public async Task<PagedViewModel<AssignmentViewModel>> SearchAsync(AssignmentSearchInputModel inputModel)
    {
        var filter = Mapper.Map<AssignmentFilter>(inputModel);
        
        var result = await _assignmentRepository.SearchAsync(_httpContextAccessor.GetUserId(), filter, inputModel.PerPage,
            inputModel.Page);

        return new PagedViewModel<AssignmentViewModel>
        {
            List = Mapper.Map<List<AssignmentViewModel>>(result.Items),
            Total = result.Total,
            Page = result.Page,
            PerPage = result.PerPage,
            PageCount = result.PageCount
        };
    }

    public async Task<AssignmentViewModel?> GetByIdAsync(int id)
    {
        var getAssignment =
            await _assignmentRepository.GetByIdAsync(id, _httpContextAccessor.GetUserId());

        if (getAssignment != null) return Mapper.Map<AssignmentViewModel>(getAssignment);

        Notificator.HandleNotFoundResource();
        return null;
    }

    public async Task<AssignmentViewModel?> CreateAsync(AddAssignmentInputModel inputModel)
    {
        var assignment = Mapper.Map<Assignment>(inputModel);
        assignment.UserId = _httpContextAccessor.GetUserId() ?? 0;

        if (!await Validate(assignment)) return null;

        _assignmentRepository.CreateAsync(assignment);

        if (await _assignmentRepository.UnityOfWork.Commit())
            return Mapper.Map<AssignmentViewModel>(assignment);

        Notificator.Handle("Não foi possível cadastrar a tarefa");
        return null;
    }

    public async Task<AssignmentViewModel?> UpdateAsync(int id, UpdateAssignmentInputModel inputModel)
    {
        if (id != inputModel.Id)
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

        var result = Mapper.Map(inputModel, getAssignment);

        if (!await Validate(result)) return null;

        _assignmentRepository.UpdateAsync(getAssignment);

        if (await _assignmentRepository.UnityOfWork.Commit())
            return Mapper.Map<AssignmentViewModel>(result);

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
        if(!assignment.Validar(out var validationResult)) 
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