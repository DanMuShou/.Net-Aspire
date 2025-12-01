using Domain.Exceptions.Rules;

namespace Domain.Entity.PostServer.Post;

/// <summary>
/// 表示一个提问问题的实体类
/// </summary>
/// <param name="title">问题标题</param>
/// <param name="content">问题内容</param>
/// <param name="askerId">提问者ID</param>
/// <param name="askerDisplayName">提问者显示名称</param>
public class PostQuestion(
    string title,
    string content,
    string askerId,
    string askerDisplayName,
    List<string> tagSlugs
)
{
    /// <summary>
    /// 获取问题的唯一标识符
    /// </summary>
    public Guid Id { get; set; } = Guid.CreateVersion7();

    /// <summary>
    /// 获取问题标题，不能为空
    /// </summary>
    public string Title { get; private set; } =
        !string.IsNullOrWhiteSpace(title) ? title : throw new EntityRuleException("标题不能为空");

    /// <summary>
    /// 获取问题内容，不能为空
    /// </summary>
    public string Content { get; private set; } =
        !string.IsNullOrWhiteSpace(content)
            ? content
            : throw new EntityRuleException("内容不能为空");

    /// <summary>
    /// 获取提问者ID，不能为空
    /// </summary>
    public string AskerId { get; private set; } =
        !string.IsNullOrWhiteSpace(askerId)
            ? askerId
            : throw new EntityRuleException("提问者ID不能为空");

    /// <summary>
    /// 获取提问者显示名称，不能为空
    /// </summary>
    public string AskerDisplayName { get; private set; } =
        !string.IsNullOrWhiteSpace(askerDisplayName)
            ? askerDisplayName
            : throw new EntityRuleException("提问者显示名称不能为空");

    /// <summary>
    /// 获取问题创建时间
    /// </summary>
    public DateTime CreateAt { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// 获取问题最后更新时间
    /// </summary>
    public DateTime? UpdateAt { get; private set; }

    /// <summary>
    /// 获取问题浏览次数
    /// </summary>
    public int ViewCount { get; private set; }

    /// <summary>
    /// 获取问题关联的标签列表
    /// </summary>
    public List<string> TagSlugs { get; private set; } =
        tagSlugs is null ? throw new EntityRuleException("标签列表不能为空") : tagSlugs;

    /// <summary>
    /// 获取问题是否有已采纳答案的标志
    /// </summary>
    public bool HasAcceptedAnswer { get; private set; }

    /// <summary>
    /// 获取问题的投票数
    /// </summary>
    public int Votes { get; private set; }

    /// <summary>
    /// 增加查看次数计数器
    /// </summary>
    public void View() => ViewCount++;

    /// <summary>
    /// 切换问题的已接受答案状态
    /// </summary>
    public void ChangeAcceptedAnswer() => HasAcceptedAnswer = !HasAcceptedAnswer;

    /// <summary>
    /// 增加投票计数器
    /// </summary>
    public void AddVotes() => Votes++;

    /// <summary>
    /// 更新当前实例的时间戳为UTC时间
    /// </summary>
    public void Update() => UpdateAt = DateTime.UtcNow;
}
