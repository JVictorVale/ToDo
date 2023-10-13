using FluentValidation;
using FluentValidation.Results;

namespace ToDo.Application.DTOs.InputModel;

public class LoginInputModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public bool Validar(out ValidationResult validationResult)
    {
        var validator = new InlineValidator<LoginInputModel>();

        validator.RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-mail deve ser fornecido")
            
            .EmailAddress()
            .WithMessage("E-mail inválido")
            
            .Length(3,255)
            .WithMessage("E-mail deve conter no mínimo {MinLength} e no máximo {MaxLength} caracteres.");

        validator.RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha deve ser fornecida")

            .Length(6, 20)
            .WithMessage("Senha deve conter no mínimo {MinLength} e no máximo {MaxLength} caracteres");

        validationResult = validator.Validate(this);
        return validationResult.IsValid;
    }
}