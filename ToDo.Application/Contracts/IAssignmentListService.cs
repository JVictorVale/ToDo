using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.DTOs.ViewModel;

namespace ToDo.Application.Contracts;

public interface IAssignmentListService
{
    Task<PagedViewModel<AssignmentListViewModel>> Search(AssignmentListSearchInputModel inputModel);
    Task<PagedViewModel<AssignmentViewModel>?> SearchAssignments(int id, AssignmentSearchInputModel inputModel);
    Task<AssignmentListViewModel?> GetById(int? id);
    Task<AssignmentListViewModel?> Create(AddAssignmentListInputModel inputModel);
    Task<AssignmentListViewModel?> Update(int id ,UpdateAssignmentListInputModel inputModel);
    Task Delete(int id);
}