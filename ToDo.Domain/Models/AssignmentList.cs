using System.Collections.ObjectModel;
using FluentValidation.Results;
using ToDo.Domain.Validators;

namespace ToDo.Domain.Models;

public class AssignmentList : BaseEntity
{
    public string Name { get; set; } = null!;
    public int UserId { get; set; }

    public virtual User User { get; set; }
    public virtual Collection<Assignment> Assignments { get; set; } = new();

    public override bool Validar(out ValidationResult validationResult)
    {
        validationResult = new AssignmentListValidator().Validate(this);
        return validationResult.IsValid;
    }
}