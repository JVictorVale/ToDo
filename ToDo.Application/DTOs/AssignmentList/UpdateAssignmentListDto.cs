namespace ToDo.Application.DTOs.InputModel;

public class UpdateAssignmentListDto : DTOs.Base.Base
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}