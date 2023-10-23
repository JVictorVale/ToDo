using ToDo.Application.DTOs.Base;

namespace ToDo.Application.DTOs.Assignment;

public class AssignmentSearchDto : BaseSearch
{
    public string? Description { get; set; } 
    public DateTime? StartDeadline { get; set; }
    public DateTime? EndDeadline { get; set; }
    public bool? Concluded { get; set; }
}