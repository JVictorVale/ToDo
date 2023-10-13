using FluentValidation;
using FluentValidation.Results;

namespace ToDo.Application.DTOs.InputModel;

public class RegisterInputModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;

    public bool Validar(out ValidationResult validationResult)
    {
        var validator = new InlineValidator<RegisterInputModel>();
        
        validator.RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome deve ser informado")
            
            .Length(3, 30)
            .WithMessage("Nome deve conter no mínimo {MinLength} e no máximo {MaxLength} caracteres.");
        
        validator.RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-mail deve ser informado.")
            
            .EmailAddress()
            .WithMessage("E-mail inválido")
            
            .Length(3, 255)
            .WithMessage("E-mail deve conter no mínimo {MinLength} e no máximo {MaxLength} caracteres.");

        validator.RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha deve ser informada.")

            .Length(6, 20)
            .WithMessage("Senha deve conter entre {MinLength} e {MaxLength} caracteres.");

        validator.RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Senha deve ser confirmada.")
            
            .Equal(x => x.Password)
            .WithMessage("Senhas não coincidem");

        validationResult = validator.Validate(this);
        return validationResult.IsValid;
    }
}