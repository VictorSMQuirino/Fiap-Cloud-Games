using FIAP_CloudGames.Domain.DTO.User;
using FluentValidation;

namespace FIAP_CloudGames.Application.Validators.User;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("User name is required.")
            .Length(2, 50).WithMessage("User name must be between 2 and 50 characters.");
        RuleFor(dto => dto.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");
        RuleFor(dto => dto.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Must(PasswordValidator.Validate)
            .WithMessage("The password must contain at least one uppercase letter, one lowercase letter, one special character, and one number.");
    }
}