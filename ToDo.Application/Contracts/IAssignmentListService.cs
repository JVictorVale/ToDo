using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.Assignment;
using ToDo.Application.DTOs.AssignmentList;
using ToDo.Application.DTOs.Paged;

namespace ToDo.Application.Contracts;

public interface IAssignmentListService
{
    Task<PagedDto<AssignmentListDto>> SearchAsync(AssignmentListSearchDto dto);
    Task<PagedDto<AssignmentDto>?> SearchAssignmentsAsync(int id, AssignmentSearchDto dto);
    Task<AssignmentListDto?> GetByIdAsync(int? id);
    Task<AssignmentListDto?> CreateAsync(AddAssignmentListDto dto);
    Task<AssignmentListDto?> UpdateAsync(int id ,UpdateAssignmentListDto dto);
    Task DeleteAsync(int id);
}