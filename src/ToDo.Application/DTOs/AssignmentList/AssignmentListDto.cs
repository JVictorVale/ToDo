using ToDo.Application.DTOs.Assignment;

namespace ToDo.Application.DTO.ViewModel;

public class AssignmentListDto : DTOs.Base.Base
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public List<AssignmentDto> AssignmentViewModels { get; set; } = new ();
}