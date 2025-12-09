using FluentValidation.Results;

namespace Application.Exceptions;

public class CustomValidationException(List<string> validationErrors) : Exception
{
    public CustomValidationException(string message)
        : this([message]) { }

    public CustomValidationException(ValidationResult validationResult)
        : this(validationResult.Errors.Select(x => x.ErrorMessage).ToList()) { }

    public List<string> ValidationErrors { get; private set; } = validationErrors;
}
