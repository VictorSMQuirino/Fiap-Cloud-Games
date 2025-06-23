using FIAP_CloudGames.Domain.DTO;
using FIAP_CloudGames.Domain.Extensions;
using FluentValidation;

namespace FIAP_CloudGames.Application.Validators.Game;

public class UpdateGameValidator : AbstractValidator<UpdateGameDto>
{
    public UpdateGameValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty().WithMessage("Title is required");
        RuleFor(dto => dto.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");
        RuleFor(dto => dto.ReleaseDate)
            .NotEmpty().WithMessage("ReleaseDate is required")
            .Must(date => date.BeAValidDate()).WithMessage("The Release Date must be a valid date");
    }
}