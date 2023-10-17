using ToDo.Application.DTOs.ViewModel;

namespace ToDo.Application.DTO.ViewModel;

public class AssignmentListViewModel : DTOs.Base.Base
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public List<AssignmentViewModel> AssignmentViewModels { get; set; } = new ();
}