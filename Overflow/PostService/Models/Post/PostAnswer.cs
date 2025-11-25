using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PostService.Models.Post;

/// <summary>
/// 表示一个问题的回答模型
/// </summary>
public class PostAnswer : BaseModel
{
    /// <summary>
    /// 回答的内容，最大长度为5000个字符
    /// </summary>
    [MaxLength(5000)]
    public required string Content { get; set; }

    /// <summary>
    /// 用户ID，最大长度为36个字符
    /// </summary>
    [MaxLength(36)]
    public required string UserId { get; set; }

    /// <summary>
    /// 用户显示名称，最大长度为300个字符
    /// </summary>
    [MaxLength(300)]
    public required string UserDisplayName { get; set; }

    /// <summary>
    /// 回答创建时间，默认为UTC当前时间
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// 是否被采纳为最佳答案的标识
    /// </summary>
    public bool IsAccepted { get; set; }

    /// <summary>
    /// 关联的问题ID，最大长度为36个字符
    /// </summary>
    [MaxLength(36)]
    public required string PostQuestionId { get; set; }

    /// <summary>
    /// 关联的问题对象，在JSON序列化时忽略此属性
    /// </summary>
    [JsonIgnore]
    public PostQuestion? PostQuestion { get; set; } = null;
}
