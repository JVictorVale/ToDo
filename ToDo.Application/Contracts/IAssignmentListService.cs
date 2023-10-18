using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.DTOs.ViewModel;

namespace ToDo.Application.Contracts;

public interface IAssignmentListService
{
    Task<PagedDto<AssignmentListDto>> Search(AssignmentListSearchDto dto);
    Task<PagedDto<AssignmentDto>?> SearchAssignments(int id, AssignmentSearchDto dto);
    Task<AssignmentListDto?> GetById(int? id);
    Task<AssignmentListDto?> Create(AddAssignmentListDto dto);
    Task<AssignmentListDto?> Update(int id ,UpdateAssignmentListDto dto);
    Task Delete(int id);
}