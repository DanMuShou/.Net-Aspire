using Application.MediatR.Commands.PostServer.PostAnswers;
using FluentValidation;

namespace Application.MediatR.Validators.PostServer;

public class CreatePostAnswerCommandValidator : AbstractValidator<CreatePostAnswerCommand>
{
    public CreatePostAnswerCommandValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("内容不能为空");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("用户ID不能为空");
        RuleFor(x => x.PostQuestionId).NotEmpty().WithMessage("问题ID不能为空");
    }
}
