using Application.MediatR.Commands.PostServer.PostAnswers;
using FluentValidation;

namespace Application.MediatR.Validators.PostServer;

public class UpdatePostAnswerCommandValidator : AbstractValidator<UpdatePostAnswerCommand>
{
    public UpdatePostAnswerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ID不能为空");
        RuleFor(x => x.Content).NotEmpty().WithMessage("内容不能为空");
    }
}
