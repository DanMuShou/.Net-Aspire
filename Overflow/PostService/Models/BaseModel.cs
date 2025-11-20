using System.ComponentModel.DataAnnotations;

namespace PostService.Models;

/// <summary>
/// BaseModel类表示一个基础模型，包含唯一标识符Id属性
/// </summary>
public class BaseModel
{
    /// <summary>
    /// 获取或设置实体的唯一标识符
    /// Id属性使用GUID生成唯一的字符串标识符，最大长度限制为36个字符
    /// </summary>
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// 实体的创建时间，默认为当前时间
    /// </summary>
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
}
