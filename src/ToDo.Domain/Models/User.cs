using System.Collections.ObjectModel;

namespace ToDo.Domain.Models;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public virtual Collection<Assignment> Assignments { get; set; } = new();
    public virtual Collection<AssignmentList> AssignmentLists { get; set; } = new();
}