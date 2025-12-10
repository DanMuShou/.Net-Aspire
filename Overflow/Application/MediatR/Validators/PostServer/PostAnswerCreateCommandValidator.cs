using Application.MediatR.Commands.PostServer.PostQuestions;
using FluentValidation;

namespace Application.MediatR.Validators.PostServer;

public class PostAnswerCreateCommandValidator : AbstractValidator<PostAnswerCreateCommand>
{
    public PostAnswerCreateCommandValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("内容不能为空");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("用户ID不能为空");
        RuleFor(x => x.PostQuestionId).NotEmpty().WithMessage("问题ID不能为空");
    }
}
