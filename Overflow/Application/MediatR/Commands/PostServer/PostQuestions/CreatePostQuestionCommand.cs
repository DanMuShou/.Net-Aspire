using MediatR;

namespace Application.MediatR.Commands.PostServer.PostQuestions;

/// <summary>
/// 创建问题帖子命令记录类，用于封装创建新问题帖子所需的数据
/// </summary>
/// <param name="Title">问题帖子的标题</param>
/// <param name="Content">问题帖子的内容</param>
/// <param name="AskedByUserId">提问用户的唯一标识符</param>
/// <param name="TagSlugs">问题关联的标签列表</param>
public record CreatePostQuestionCommand(
    string Title,
    string Content,
    string AskedByUserId,
    List<string> TagSlugs
) : IRequest<Guid>;
