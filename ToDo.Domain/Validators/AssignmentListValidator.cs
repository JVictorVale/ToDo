using FluentValidation;
using ToDo.Domain.Models;

namespace ToDo.Domain.Validators;

public class AssignmentListValidator : AbstractValidator<AssignmentList>
{
    public AssignmentListValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .WithMessage("Nome deve ser informado.")

            .Length(3, 60)
            .WithMessage("Nome deve conter no mínimo 3 caracteres e no máximo 60 caracteres");

        RuleFor(a => a.UserId)
            .NotEmpty()
            .NotEqual(0)
            .WithMessage("Id do usuário deve ser informado.");
        
        RuleFor(a=> a.CreateAt)
            .Must( createAt => createAt.Date >= DateTime.Today)
            .WithMessage("A data de expiração não pode ser menor que a data de hoje.");
        
        RuleFor(a=> a.UpdateAt)
            .Must( updateAtt => updateAtt.Date >= DateTime.Today)
            .WithMessage("A data de expiração não pode ser menor que a data de hoje.");
    }
}