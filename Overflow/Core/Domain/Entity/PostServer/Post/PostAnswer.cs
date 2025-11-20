using Domain.Exceptions.Rules;

namespace Domain.Entity.PostServer.Post;

public class PostAnswer(string content, string userId, string userDisplayName, Guid postQuestionId)
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();

    public string Content { get; private set; } =
        !string.IsNullOrWhiteSpace(content)
            ? content
            : throw new EntityRuleException("内容不能为空");

    public string UserId { get; private set; } =
        !string.IsNullOrWhiteSpace(userId)
            ? userId
            : throw new EntityRuleException("用户ID不能为空");

    public string UserDisplayName { get; private set; } =
        !string.IsNullOrWhiteSpace(userDisplayName)
            ? userDisplayName
            : throw new EntityRuleException("用户显示名称不能为空");

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public bool IsAccepted { get; private set; }

    public Guid PostQuestionId { get; private set; } =
        postQuestionId == Guid.Empty
            ? throw new EntityRuleException("关联的问题ID不能为空")
            : postQuestionId;

    public PostQuestion? PostQuestion { get; private set; } = null;

    public void ChangeAccept() => IsAccepted = !IsAccepted;
}
