using FluentValidation;
using ToDo.Domain.Models;

namespace ToDo.Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .WithMessage("O nome do usuário deve ser informado.")

            .Length(3, 60)
            .WithMessage("Nome deve conter no mínimo 3 caracteres e no máximo 60 caracteres.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("E-mail do usuário deve ser informado.")

            .Length(3, 255)
            .WithMessage("E-mail deve conter no mínimo 3 caracteres e no máximo 255 caracteres");

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Senha do usuário deve ser informada.")

            .Length(6, 20)
            .WithMessage("Senha deve conter no mínimo 6 caracteres e no máximo 20 caraceteres");
        
        RuleFor(u=> u.CreateAt)
            .Must( createAt => createAt.Date >= DateTime.Today)
            .WithMessage("A data de expiração não pode ser menor que a data de hoje.");
        
        RuleFor(u=> u.UpdateAt)
            .Must( updateAtt => updateAtt.Date >= DateTime.Today)
            .WithMessage("A data de expiração não pode ser menor que a data de hoje.");
    }
}