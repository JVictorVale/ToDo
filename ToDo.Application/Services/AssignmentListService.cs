using AutoMapper;
using Microsoft.AspNetCore.Http;
using ToDo.Application.Contracts;
using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.DTOs.ViewModel;
using ToDo.Application.Extensions;
using ToDo.Application.Notification;
using ToDo.Domain.Contracts.Interfaces;
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

    public async Task<PagedViewModel<AssignmentListViewModel>> Search(AssignmentListSearchInputModel inputModel)
    {
        var result = await _assignmentListRepository.Search(_httpContextAccessor.GetUserId(), inputModel.Name,
            inputModel.Description, inputModel.PerPage, inputModel.Page);

        return new PagedViewModel<AssignmentListViewModel>
        {
            List = Mapper.Map<List<AssignmentListViewModel>>(result.Items),
            Total = result.Total,
            Page = result.Page,
            PerPage = result.PerPage,
            PageCount = result.PageCount
        };
    }

    public async Task<PagedViewModel<AssignmentViewModel>?> SearchAssignments(int id, AssignmentSearchInputModel inputModel)
    {
        var httpAccessor = _httpContextAccessor.GetUserId();
        var filter = Mapper.Map<AssignmentFilter>(inputModel);

        var getAssignment = await _assignmentListRepository.GetById(id, httpAccessor);

        if (getAssignment == null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }

        var result = await _assignmentRepository
            .SearchAsync(httpAccessor, filter, inputModel.PerPage, inputModel.Page, id);

        return new PagedViewModel<AssignmentViewModel>
        {
            List = Mapper.Map<List<AssignmentViewModel>>(result.Items),
            Total = result.Total,
            Page = result.Page,
            PerPage = result.PerPage,
            PageCount = result.PageCount
        };
    }

    public async Task<AssignmentListViewModel?> GetById(int? id)
    {
        var getAssignmentList = await _assignmentListRepository.GetById(id, _httpContextAccessor.GetUserId());

        if (getAssignmentList != null) return Mapper.Map<AssignmentListViewModel>(getAssignmentList);

        Notificator.HandleNotFoundResource();
        return null;
    }

    public async Task<AssignmentListViewModel?> Create(AddAssignmentListInputModel inputModel)
    {
        var assignmentList = Mapper.Map<AssignmentList>(inputModel);
        assignmentList.UserId = _httpContextAccessor.GetUserId() ?? 0;

        if (!await Validate(assignmentList)) return null;

        _assignmentListRepository.CreateAsync(assignmentList);

        if (await _assignmentListRepository.UnityOfWork.Commit())
            return Mapper.Map<AssignmentListViewModel>(assignmentList);

        Notificator.Handle("Não foi possível criar a lista de tarefa");
        return null;
    }

    public async Task<AssignmentListViewModel?> Update(int id, UpdateAssignmentListInputModel inputModel)
    {
        if (id != inputModel.Id)
        {
            Notificator.Handle("Os IDs não conferem.");
            return null;
        }

        var getAssignmentList = await _assignmentListRepository.GetById(id, _httpContextAccessor.GetUserId());

        if (getAssignmentList == null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }

        Mapper.Map(inputModel, getAssignmentList);

        if (!await Validate(getAssignmentList))
        {
            return null;
        }

        _assignmentListRepository.UpdateAsync(getAssignmentList);

        if (await _assignmentListRepository.UnityOfWork.Commit())
        {
            return Mapper.Map<AssignmentListViewModel>(getAssignmentList);
        }

        Notificator.Handle("Não foi possível atualizar a lista de tarefa.");
        return null;
    }

    public async Task Delete(int id)
    {
        var getAssignmentList = await _assignmentListRepository.GetById(id, _httpContextAccessor.GetUserId());

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

        _assignmentListRepository.DeleteAsync(getAssignmentList);

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