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
        
        RuleFor(a => a.ConcludedAt)
            .Must( concludedAt => concludedAt != null && concludedAt.Value.Date >= DateTime.Today)
            .WithMessage("A data de expiração não pode ser menor que a data de hoje.");
        
        RuleFor(a => a.Deadline)
            .Must( deadLine => deadLine != null && deadLine.Value.Date >= DateTime.Today)
            .WithMessage("A data de expiração não pode ser menor que a data de hoje.");
        
        RuleFor(a=> a.CreateAt)
            .Must( createAt => createAt.Date >= DateTime.Today)
            .WithMessage("A data de expiração não pode ser menor que a data de hoje.");
        
        RuleFor(a=> a.UpdateAt)
            .Must( updateAtt => updateAtt.Date >= DateTime.Today)
            .WithMessage("A data de expiração não pode ser menor que a data de hoje.");
    }
}