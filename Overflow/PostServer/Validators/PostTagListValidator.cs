using System.ComponentModel.DataAnnotations;

namespace PostServer.Validators;

public class PostTagListValidator(int min, int max) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is List<string> tags && tags.Count >= min && tags.Count <= max)
            return ValidationResult.Success;
        return new ValidationResult($"标签数量必须在{min}到{max}之间");
    }
}
