using FluentValidation.Results;

namespace FIAP_CloudGames.Domain.Extensions;

public static class ValidationExtensions
{
    public static IDictionary<string, string[]> ConvertValidationErrors(this List<ValidationFailure> errors)
    {
        return errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key, 
                g => g.Select(e => e.ErrorMessage).ToArray());
    }
}