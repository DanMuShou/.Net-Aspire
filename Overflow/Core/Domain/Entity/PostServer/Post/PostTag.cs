using Domain.Exceptions.Rules;

namespace Domain.Entity.PostServer.Post;

/// <summary>
/// 表示一个帖子标签实体类
/// </summary>
/// <param name="name">标签名称</param>
/// <param name="slug">标签别名</param>
/// <param name="description">标签描述</param>
public class PostTag(string name, string slug, string description)
{
    /// <summary>
    /// 获取标签的唯一标识符
    /// </summary>
    public Guid Id { get; set; } = Guid.CreateVersion7();

    /// <summary>
    /// 获取标签名称
    /// </summary>
    public string Name { get; private set; } =
        !string.IsNullOrWhiteSpace(name) ? name : throw new EntityRuleException("标签名称不能为空");

    /// <summary>
    /// 获取标签别名
    /// </summary>
    public string Slug { get; private set; } =
        !string.IsNullOrWhiteSpace(slug) ? slug : throw new EntityRuleException("标签别名不能为空");

    /// <summary>
    /// 获取标签描述
    /// </summary>
    public string Description { get; private set; } =
        !string.IsNullOrWhiteSpace(description)
            ? description
            : throw new EntityRuleException("标签描述不能为空");

    /// <summary>
    /// 获取标签创建时间
    /// </summary>
    public DateTime CreateAt { get; private set; } = DateTime.UtcNow;
}
