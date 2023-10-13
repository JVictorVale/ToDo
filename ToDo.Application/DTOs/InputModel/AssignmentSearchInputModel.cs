using ToDo.Application.DTOs.Base;

namespace ToDo.Application.DTOs.InputModel;

public class AssignmentSearchInputModel : BaseSearch
{
    public string Description { get; set; } = null!;
    public DateTime? StartDeadline { get; set; }
    public DateTime? EndDeadline { get; set; }
    public bool? Concluded { get; set; }
}