using FluentValidation;
using ToDo.Domain.Models;

namespace ToDo.Domain.Validators;

public class AssignmentValidator : AbstractValidator<Assignment>
{
    public AssignmentValidator()
    {
        RuleFor(a => a.Description)
            .NotEmpty()
            .WithMessage("Descrição deve ser informada.")

            .Length(3, 300)
            .WithMessage("Descrição deve conter no mínimo 3 caracteres e no máximo 300 caracteres.");

        RuleFor(a => a.UserId)
            .NotEmpty()
            .NotEqual(0)
            .WithMessage("Id do usuário deve ser informado.");

        RuleFor(a => a.AssignmentListId)
            .NotEqual(0)
            .WithMessage("Id inválido.");
        
        RuleFor(a => a.Deadline)
            .NotEmpty()
            .WithMessage("O prazo final não pode ser vazio.");
    }
}