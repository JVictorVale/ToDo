namespace ToDo.Application.DTOs.Assignment;

public class UpdateAssignmentDto : DTOs.Base.Base
{
    public string Description { get; set; } = null!;
    public DateTime Deadline { get; set; }
    public int AssignmentListId { get; set; }
}