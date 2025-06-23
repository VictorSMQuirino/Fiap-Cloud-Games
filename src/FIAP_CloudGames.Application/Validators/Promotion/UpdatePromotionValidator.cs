using FIAP_CloudGames.Domain.DTO.Promotion;
using FIAP_CloudGames.Domain.Extensions;
using FluentValidation;

namespace FIAP_CloudGames.Application.Validators.Promotion;

public class UpdatePromotionValidator : AbstractValidator<UpdatePromotionDto>
{
    public UpdatePromotionValidator()
    {
        RuleFor(promotion => promotion.DiscountPercentage)
            .NotNull().WithMessage("The discount percentage is required.")
            .GreaterThan(0).WithMessage("The discount percentage must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("The discount percentage must be less than or equal to 100.");
        RuleFor(promotion => promotion.Deadline)
            .NotEmpty().WithMessage("The deadline is required.")
            .Must(date => date.BeAValidDate()).WithMessage("The deadline must be a valid date.");
    }
}