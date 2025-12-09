using Domain.Exceptions.Rules;

namespace Domain.Entity.PostServer.Post;

/// <summary>
/// 表示一个帖子标签实体类
/// </summary>
public class PostTag
{
    /// <summary>
    /// 初始化 <see cref="PostTag"/> 类的新实例
    /// </summary>
    /// <param name="name">标签名称</param>
    /// <param name="slug">标签别名</param>
    /// <param name="description">标签描述</param>
    /// <exception cref="EntityRuleException">当参数不符合验证规则时抛出</exception>
    public PostTag(string name, string slug, string description)
    {
        if (string.IsNullOrEmpty(name))
            throw new EntityRuleException("标签名称不能为空");
        if (string.IsNullOrEmpty(slug))
            throw new EntityRuleException("标签别名不能为空");
        if (string.IsNullOrEmpty(description))
            throw new EntityRuleException("标签描述不能为空");

        Id = Guid.CreateVersion7();
        Name = name;
        Slug = slug;
        Description = description;
        CreateAt = DateTime.UtcNow;
    }

    /// <summary>
    /// 获取标签的唯一标识符
    /// </summary>
    /// <value>标签的唯一标识符</value>
    public Guid Id { get; private set; }

    /// <summary>
    /// 获取标签名称
    /// </summary>
    /// <value>标签的名称</value>
    public string Name { get; private set; }

    /// <summary>
    /// 获取标签别名
    /// </summary>
    /// <value>标签的别名，通常用于URL友好展示</value>
    public string Slug { get; private set; }

    /// <summary>
    /// 获取标签描述
    /// </summary>
    /// <value>标签的详细描述信息</value>
    public string Description { get; private set; }

    /// <summary>
    /// 获取标签创建时间
    /// </summary>
    /// <value>UTC时间格式的标签创建时间</value>
    public DateTime CreateAt { get; private set; }
}