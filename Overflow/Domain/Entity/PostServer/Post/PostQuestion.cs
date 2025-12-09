using Domain.Exceptions.Rules;

namespace Domain.Entity.PostServer.Post;

/// <summary>
/// 表示一个提问问题的实体类
/// </summary>
public class PostQuestion
{
    /// <summary>
    /// 初始化 <see cref="PostQuestion"/> 类的新实例
    /// </summary>
    /// <param name="title">问题标题</param>
    /// <param name="content">问题内容</param>
    /// <param name="askerId">提问者ID</param>
    /// <param name="askerDisplayName">提问者显示名称</param>
    /// <param name="tagSlugs">问题标签列表</param>
    /// <exception cref="EntityRuleException">当参数不符合验证规则时抛出</exception>
    public PostQuestion(string title, string content, string askerId, List<string> tagSlugs)
    {
        ValidateTitle(title);
        ValidateContent(content);
        ValidateTagSlugs(tagSlugs);

        if (string.IsNullOrWhiteSpace(title))
            throw new EntityRuleException("标题不能为空");
        if (tagSlugs.Count == 0)
            throw new EntityRuleException("标签列表不能为空");

        Id = Guid.CreateVersion7();
        Title = title;
        Content = content;
        AskerId = askerId;
        CreateAt = DateTime.UtcNow;
        TagSlugs = tagSlugs;
        Votes = 0;
        HasAcceptedAnswer = false;
    }

    /// <summary>
    /// 获取问题的唯一标识符
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// 获取或设置问题标题
    /// </summary>
    /// <value>问题标题，不能为空</value>
    public string Title { get; private set; }

    /// <summary>
    /// 获取或设置问题内容
    /// </summary>
    /// <value>问题内容，不能为空</value>
    public string Content { get; private set; }

    /// <summary>
    /// 获取或设置提问者ID
    /// </summary>
    /// <value>提问者ID，不能为空</value>
    public string AskerId { get; private set; }

    /// <summary>
    /// 获取问题创建时间
    /// </summary>
    /// <value>UTC时间格式的问题创建时间</value>
    public DateTime CreateAt { get; private set; }

    /// <summary>
    /// 获取或设置问题最后更新时间
    /// </summary>
    /// <value>UTC时间格式的问题最后更新时间，可能为空</value>
    public DateTime? UpdateAt { get; private set; }

    /// <summary>
    /// 获取或设置问题浏览次数
    /// </summary>
    /// <value>问题被浏览的次数</value>
    public int ViewCount { get; private set; }

    /// <summary>
    /// 获取或设置问题关联的标签列表
    /// </summary>
    /// <value>问题关联的标签slug列表</value>
    public List<string> TagSlugs { get; private set; }

    /// <summary>
    /// 获取或设置问题是否有已采纳答案的标志
    /// </summary>
    /// <value>如果问题有已采纳的答案则为true，否则为false</value>
    public bool HasAcceptedAnswer { get; private set; }

    /// <summary>
    /// 获取或设置问题的投票数
    /// </summary>
    /// <value>问题获得的投票总数</value>
    public int Votes { get; private set; }

    /// <summary>
    /// 增加问题的浏览次数
    /// </summary>
    public void View() => ViewCount++;

    /// <summary>
    /// 增加问题的投票数
    /// </summary>
    public void AddVotes() => Votes++;

    /// <summary>
    /// 更新问题标题
    /// </summary>
    /// <param name="title">新的问题标题</param>
    /// <exception cref="EntityRuleException">当标题为空时抛出</exception>
    public void UpdateTitle(string title)
    {
        ValidateTitle(title);
        Title = title;
    }

    /// <summary>
    /// 更新问题内容
    /// </summary>
    /// <param name="content">新的问题内容</param>
    /// <exception cref="EntityRuleException">当内容为空时抛出</exception>
    public void UpdateContent(string content)
    {
        ValidateContent(content);
        Content = content;
    }

    /// <summary>
    /// 更新问题标签列表
    /// </summary>
    /// <param name="tagSlugs">新的标签列表</param>
    /// <exception cref="EntityRuleException">当标签列表为空时抛出</exception>
    public void UpdateTagSlugs(List<string> tagSlugs)
    {
        ValidateTagSlugs(tagSlugs);
        TagSlugs = tagSlugs;
    }

    /// <summary>
    /// 更新问题是否有已采纳答案的状态
    /// </summary>
    /// <param name="value">是否有已采纳答案</param>
    public void UpdateHasAcceptedAnswer(bool value) => HasAcceptedAnswer = value;

    /// <summary>
    /// 更新问题的最后更新时间
    /// </summary>
    public void Update() => UpdateAt = DateTime.UtcNow;

    /// <summary>
    /// 验证问题标题
    /// </summary>
    /// <param name="title">要验证的标题</param>
    /// <exception cref="EntityRuleException">当标题为空时抛出</exception>
    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new EntityRuleException("标题不能为空");
    }

    /// <summary>
    /// 验证问题内容
    /// </summary>
    /// <param name="content">要验证的内容</param>
    /// <exception cref="EntityRuleException">当内容为空时抛出</exception>
    private static void ValidateContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new EntityRuleException("内容不能为空");
    }

    /// <summary>
    /// 验证标签列表
    /// </summary>
    /// <param name="tagSlugs">要验证的标签列表</param>
    /// <exception cref="EntityRuleException">当标签列表为空时抛出</exception>
    private static void ValidateTagSlugs(List<string> tagSlugs)
    {
        if (tagSlugs is null || tagSlugs.Count == 0)
            throw new EntityRuleException("标签列表不能为空");
    }
}
