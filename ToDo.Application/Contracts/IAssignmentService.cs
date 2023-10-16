using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;

namespace ToDo.Application.Contracts;

public interface IAssignmentService
{
    Task<PagedViewModel<AssignmentViewModel>> SearchAsync(AssignmentSearchInputModel inputModel);
    Task<AssignmentViewModel?> GetByIdAsync(int id);
    Task<AssignmentViewModel?> CreateAsync(AddAssignmentInputModel inputModel);
    Task<AssignmentViewModel?> UpdateAsync(int id, UpdateAssignmentInputModel inputModel);
    Task DeleteAsync(int id);
    Task MarkConcludedAsync(int id);
    Task MarkDesconcludedAsync(int id);
}