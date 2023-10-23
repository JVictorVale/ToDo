namespace ToDo.Application.DTOs.Assignment;

public class AddAssignmentDto
{
    public string Description { get; set; } = null!;
    public DateTime Deadline { get; set; } 
    public int AssignmentListId { get; set; }
}