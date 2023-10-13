namespace ToDo.Application.DTOs.InputModel;

public class UpdateAssignmentInputModel : DTOs.Base.Base
{
    public string Description { get; set; } = null!;
    public DateTime Deadline { get; set; }
    public int AssignmentListId { get; set; }
}