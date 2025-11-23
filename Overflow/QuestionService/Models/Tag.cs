using System.ComponentModel.DataAnnotations;

namespace QuestionService.Models;

/// <summary>
/// 标签实体类，用于表示问题的标签信息
/// </summary>
public class Tag
{
    /// <summary>
    /// 标签唯一标识符，使用GUID生成，默认最大长度36个字符
    /// </summary>
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// 标签名称，最大长度50个字符，为必填字段
    /// </summary>
    [MaxLength(50)]
    public required string Name { get; set; }

    /// <summary>
    /// 标签别名，通常用于URL友好展示，最大长度50个字符，为必填字段
    /// </summary>
    [MaxLength(50)]
    public required string Slug { get; set; }

    /// <summary>
    /// 标签描述信息，最大长度1000个字符，为必填字段
    /// </summary>
    [MaxLength(1000)]
    public required string Description { get; set; }
}
