namespace ToDo.Application.DTO.ViewModel;

public class AssignmentViewModel : DTOs.Base.Base
{
    public string Description { get; set; } = null!;
    public int? AssignmentListId { get; set; }
    public bool Concluded { get; set; }
    public DateTime? ConcludedAt { get; set; }
    public DateTime? Deadline { get; set; }
}