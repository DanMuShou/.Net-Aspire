using System.ComponentModel.DataAnnotations;

namespace PostService.Models.Post;

/// <summary>
/// 问题实体类，用于表示系统中的问题信息
/// </summary>
public class PostQuestion : BaseModel
{
    /// <summary>
    /// 问题标题，最大长度300个字符，必需字段
    /// </summary>
    [MaxLength(300)]
    public required string Title { get; set; }

    /// <summary>
    /// 问题内容，最大长度5000个字符，必需字段
    /// </summary>
    [MaxLength(5000)]
    public required string Content { get; set; }

    /// <summary>
    /// 提问者用户ID，最大长度36个字符，必需字段
    /// </summary>
    [MaxLength(36)]
    public required string AskerId { get; set; }

    /// <summary>
    /// 提问者显示名称，最大长度300个字符，必需字段
    /// </summary>
    [MaxLength(300)]
    public required string AskerDisplayName { get; set; }

    /// <summary>
    /// 问题最后更新时间，可为空
    /// </summary>
    public DateTime? UpdateAt { get; set; }

    /// <summary>
    /// 问题浏览次数
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// 问题关联的标签slug列表
    /// </summary>
    public List<string> TagSlugs { get; set; } = [];

    /// <summary>
    /// 标识问题是否有已接受的答案
    /// </summary>
    public bool HasAcceptedAnswer { get; set; }

    /// <summary>
    /// 问题获得的投票数
    /// </summary>
    public int Votes { get; set; }
}
