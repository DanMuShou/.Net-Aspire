using Application.MediatR.Commands.PostServer.PostQuestions;
using FluentValidation;

namespace Application.MediatR.Validators.PostServer;

public class PostAnswerUpdateCommandValidator : AbstractValidator<PostAnswerUpdateCommand>
{
    public PostAnswerUpdateCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ID不能为空");
        RuleFor(x => x.Content).NotEmpty().WithMessage("内容不能为空");
    }
}
