using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.DTOs.ViewModel;

namespace ToDo.Application.Contracts;

public interface IAssignmentService
{
    Task<PagedDto<AssignmentDto>> SearchAsync(AssignmentSearchDto dto);
    Task<AssignmentDto?> GetByIdAsync(int id);
    Task<AssignmentDto?> CreateAsync(AddAssignmentDto dto);
    Task<AssignmentDto?> UpdateAsync(int id, UpdateAssignmentDto dto);
    Task DeleteAsync(int id);
    Task MarkConcludedAsync(int id);
    Task MarkDesconcludedAsync(int id);
}