using FluentValidation.Results;
using ToDo.Domain.Validators;

namespace ToDo.Domain.Models;

public class Assignment : BaseEntity
{
    public string Description { get; set; } = null!;
    public int UserId { get; set; }
    public int? AssignmentListId { get; set; }
    public bool Concluded { get; set; }
    public DateTime? ConcludedAt { get; set; }
    public DateTime? Deadline { get; set; }

    public User User { get; set; } = null!;
    public AssignmentList AssignmentList { get; set; } = null!;
    
    public void SetConcluded()
    {
        Concluded = true;
        ConcludedAt = DateTime.Now;
    }

    public void SetUnconcluded()
    {
        Concluded = false;
        ConcludedAt = null;
    }

    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new AssignmentValidator().Validate(this);
        return validationResult.IsValid;
    }
}