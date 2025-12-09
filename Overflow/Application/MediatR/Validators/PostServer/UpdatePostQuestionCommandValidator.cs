using Application.MediatR.Commands.PostServer.PostQuestions;
using FluentValidation;

namespace Application.MediatR.Validators.PostServer;

public class UpdatePostQuestionCommandValidator : AbstractValidator<UpdatePostQuestionCommand>
{
    public UpdatePostQuestionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ID不能为空");
        RuleFor(x => x.Title).NotEmpty().WithMessage("标题不能为空。");
        RuleFor(x => x.Content).NotEmpty().WithMessage("内容不能为空。");
        RuleFor(x => x.Tags)
            .NotEmpty()
            .WithMessage("标签列表不能为空。")
            .Must(tags => tags.Count is >= 1 and <= 5)
            .WithMessage("标签数量必须在1到5之间");
        RuleFor(x => x.HasAcceptedAnswer).NotNull().WithMessage("是否已采纳答案不能为空。");
    }
}
