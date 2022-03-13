using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Validators;

public class MinimumAllowedYearAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // Get the user entered value
        var userEnteredYear = ((DateTime) value).Year;

        if (userEnteredYear < 1900)
        {
            return new ValidationResult("Year should be no less than 1900");
        }

        return ValidationResult.Success;
    }
}