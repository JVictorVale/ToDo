using FluentValidation;
using ToDo.Domain.Models;

namespace ToDo.Domain.Validators;

public class AssignmentValidator : AbstractValidator<Assignment>
{
    public AssignmentValidator()
    {
        RuleFor(a => a.Description)
            .NotEmpty()
            .WithMessage("A descrição deve ser informada.")

            .Length(3, 300)
            .WithMessage("A descrição deve conter no mínimo {MinLength} e no máximo {MaxLength} caracteres.");

        RuleFor(a => a.Deadline)
            .GreaterThan(DateTime.Now)
            .WithMessage("O prazo final deve ser maior que a data de hoje.");

        RuleFor(a => a.UserId)
            .NotEmpty()
            .NotEqual(0)
            .WithMessage("O id do usuário deve ser informado.");
    }
}