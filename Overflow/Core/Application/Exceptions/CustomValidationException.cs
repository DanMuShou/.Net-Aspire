using FluentValidation.Results;

namespace Application.Exceptions;

public class CustomValidationException(ValidationResult validationResults) : Exception
{
    public List<string> ValidationErrors { get; set; } =
        validationResults.Errors.Select(x => x.ErrorMessage).ToList();
}
