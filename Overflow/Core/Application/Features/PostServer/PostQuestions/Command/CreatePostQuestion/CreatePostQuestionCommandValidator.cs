using FluentValidation;

namespace Application.Features.PostServer.PostQuestions.Command.CreatePostQuestion;

/// <summary>
/// 创建提问命令的验证器类
/// 用于验证CreatePostQuestionCommand对象的各个属性是否符合业务规则
/// </summary>
public class CreatePostQuestionCommandValidator : AbstractValidator<CreatePostQuestionCommand>
{
    /// <summary>
    /// 初始化CreatePostQuestionCommandValidator实例
    /// 配置针对CreatePostQuestionCommand对象各属性的验证规则
    /// </summary>
    public CreatePostQuestionCommandValidator()
    {
        // 验证标题字段不能为空
        RuleFor(x => x.Title).NotEmpty().WithMessage("标题不能为空。");

        // 验证内容字段不能为空
        RuleFor(x => x.Content).NotEmpty().WithMessage("内容不能为空。");

        // 验证提问者ID字段不能为空
        RuleFor(x => x.AskedByUserId).NotEmpty().WithMessage("提问者ID不能为空。");

        // 验证提问者显示名称字段不能为空
        RuleFor(x => x.AskedByUserDisplayName).NotEmpty().WithMessage("提问者显示名称不能为空。");

        // 验证标签列表字段不能为空
        RuleFor(x => x.TagSlugs).NotEmpty().WithMessage("标签列表不能为空。");
    }
}
