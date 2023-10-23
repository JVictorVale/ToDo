using FluentValidation;
using ToDo.Domain.Models;

namespace ToDo.Domain.Validators;

public class AssignmentListValidator : AbstractValidator<AssignmentList>
{
    public AssignmentListValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .WithMessage("O nome da lista de tarefas deve ser informada.");
            
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("A descrição da lista de tarefas não pode ser vazia.");
    }
}