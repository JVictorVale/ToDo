using AutoMapper;
using Microsoft.AspNetCore.Http;
using ToDo.Application.Contracts;
using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.Assignment;
using ToDo.Application.DTOs.AssignmentList;
using ToDo.Application.DTOs.Paged;
using ToDo.Application.Extensions;
using ToDo.Application.Notification;
using ToDo.Domain.Contracts.Repositories;
using ToDo.Domain.Filter;
using ToDo.Domain.Models;

namespace ToDo.Application.Services;

public class AssignmentListService : BaseService, IAssignmentListService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IAssignmentListRepository _assignmentListRepository;
    
    public AssignmentListService(IMapper mapper, INotificator notificator, IHttpContextAccessor httpContextAccessor, IAssignmentRepository assignmentRepository, IAssignmentListRepository assignmentListRepository) : base(mapper, notificator)
    {
        _httpContextAccessor = httpContextAccessor;
        _assignmentRepository = assignmentRepository;
        _assignmentListRepository = assignmentListRepository;
    }

    public async Task<PagedDto<AssignmentListDto>> SearchAsync(AssignmentListSearchDto dto)
    {
        var result = await _assignmentListRepository.SearchAsync(_httpContextAccessor.GetUserId(), dto.Name,
            dto.Description, dto.PerPage, dto.Page);

        return new PagedDto<AssignmentListDto>
        {
            List = Mapper.Map<List<AssignmentListDto>>(result.Items),
            Total = result.Total,
            Page = result.Page,
            PerPage = result.PerPage,
            PageCount = result.PageCount
        };
    }

    public async Task<PagedDto<AssignmentDto>?> SearchAssignmentsAsync(int id, AssignmentSearchDto dto)
    {
        var httpAccessor = _httpContextAccessor.GetUserId();
        var filter = Mapper.Map<AssignmentFilter>(dto);

        var getAssignment = await _assignmentListRepository.GetByIdAsync(id, httpAccessor);

        if (getAssignment == null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }

        var result = await _assignmentRepository
            .SearchAsync(httpAccessor, filter, dto.PerPage, dto.Page, id);

        return new PagedDto<AssignmentDto>
        {
            List = Mapper.Map<List<AssignmentDto>>(result.Items),
            Total = result.Total,
            Page = result.Page,
            PerPage = result.PerPage,
            PageCount = result.PageCount
        };
    }

    public async Task<AssignmentListDto?> GetByIdAsync(int? id)
    {
        var getAssignmentList = await _assignmentListRepository.GetByIdAsync(id, _httpContextAccessor.GetUserId());

        if (getAssignmentList != null) return Mapper.Map<AssignmentListDto>(getAssignmentList);

        Notificator.HandleNotFoundResource();
        return null;
    }

    public async Task<AssignmentListDto?> CreateAsync(AddAssignmentListDto dto)
    {
        var assignmentList = Mapper.Map<AssignmentList>(dto);
        assignmentList.UserId = _httpContextAccessor.GetUserId() ?? 0;

        if (!await Validate(assignmentList)) return null;

        _assignmentListRepository.Create(assignmentList);

        if (await _assignmentListRepository.UnityOfWork.Commit())
            return Mapper.Map<AssignmentListDto>(assignmentList);

        Notificator.Handle("Não foi possível criar a lista de tarefa");
        return null;
    }

    public async Task<AssignmentListDto?> UpdateAsync(int id, UpdateAssignmentListDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("Os IDs não conferem.");
            return null;
        }

        var getAssignmentList = await _assignmentListRepository.GetByIdAsync(id, _httpContextAccessor.GetUserId());

        if (getAssignmentList == null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }

        Mapper.Map(dto, getAssignmentList);

        if (!await Validate(getAssignmentList))
        {
            return null;
        }

        _assignmentListRepository.Update(getAssignmentList);

        if (await _assignmentListRepository.UnityOfWork.Commit())
        {
            return Mapper.Map<AssignmentListDto>(getAssignmentList);
        }

        Notificator.Handle("Não foi possível atualizar a lista de tarefa.");
        return null;
    }

    public async Task DeleteAsync(int id)
    {
        var getAssignmentList = await _assignmentListRepository.GetByIdAsync(id, _httpContextAccessor.GetUserId());

        if (getAssignmentList == null)
        {
            Notificator.HandleNotFoundResource();
            return;
        }

        if (getAssignmentList.Assignments.Any(x => !x.Concluded))
        {
            Notificator.Handle("Não é possível excluir lista com tarefas não concluídas.");
            return;
        }

        _assignmentListRepository.Delete(getAssignmentList);

        if (!await _assignmentListRepository.UnityOfWork.Commit())
        {
            Notificator.Handle("Não foi possível remover a lista de tarefa.");
        }
    }
    
    private async Task<bool> Validate(AssignmentList assignmentList)
    {
        if (!assignmentList.Validate(out var validationResult))
            Notificator.Handle(validationResult.Errors);

        var assignmentExistent = await _assignmentListRepository.FirstOrDefaultAsync(u =>
            u.Name == assignmentList.Name);

        if (assignmentExistent != null)
            Notificator.Handle("Já existe uma lista de tarefa com esse nome.");

        return !Notificator.HasNotification;
    }
}