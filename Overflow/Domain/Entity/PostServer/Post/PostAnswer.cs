using Domain.Exceptions.Rules;

namespace Domain.Entity.PostServer.Post;

/// <summary>
/// 表示一个问题的回答实体类
/// </summary>
public class PostAnswer
{
    /// <summary>
    /// 初始化 <see cref="PostAnswer"/> 类的新实例
    /// </summary>
    /// <param name="content">回答内容</param>
    /// <param name="userId">回答者用户ID</param>
    /// <param name="postQuestionId">所回答问题的ID</param>
    /// <exception cref="EntityRuleException">当参数不符合验证规则时抛出</exception>
    public PostAnswer(string content, string userId, Guid postQuestionId)
    {
        ValidContent(content);

        if (string.IsNullOrWhiteSpace(userId))
            throw new EntityRuleException("用户ID不能为空");
        if (postQuestionId == Guid.Empty)
            throw new EntityRuleException("问题ID不能为空");

        Id = Guid.CreateVersion7();
        Content = content;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        IsAccepted = false;
        PostQuestionId = postQuestionId;
    }

    /// <summary>
    /// 获取回答的唯一标识符
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// 获取或设置回答内容
    /// </summary>
    /// <value>回答内容，不能为空</value>
    public string Content { get; private set; }

    /// <summary>
    /// 获取或设置回答者用户ID
    /// </summary>
    /// <value>回答者的用户ID</value>
    public string UserId { get; private set; }

    /// <summary>
    /// 获取回答创建时间
    /// </summary>
    /// <value>UTC时间格式的回答创建时间</value>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// 获取回答更新时间
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// 获取或设置回答是否被采纳为最佳答案
    /// </summary>
    /// <value>如果回答被采纳为最佳答案则为true，否则为false</value>
    public bool IsAccepted { get; private set; }

    /// <summary>
    /// 获取或设置所回答问题的ID
    /// </summary>
    /// <value>关联的问题ID</value>
    public Guid PostQuestionId { get; private set; }

    /// <summary>
    /// 设置回答是否被采纳为最佳答案
    /// </summary>
    /// <param name="value">是否被采纳为最佳答案</param>
    public void SetAccepted(bool value) => IsAccepted = value;

    public void Update() => UpdatedAt = DateTime.UtcNow;

    /// <summary>
    /// 更新回答内容
    /// </summary>
    /// <param name="content">新的回答内容</param>
    /// <exception cref="EntityRuleException">当内容为空时抛出</exception>
    public void UpdateContent(string content)
    {
        ValidContent(content);
        Content = content;
    }

    /// <summary>
    /// 验证回答内容
    /// </summary>
    /// <param name="content">要验证的内容</param>
    /// <exception cref="EntityRuleException">当内容为空时抛出</exception>
    private static void ValidContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new EntityRuleException("内容不能为空");
    }
}
