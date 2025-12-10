namespace Application.Contracts.Queues.Post;

// 传递给问题服务总线的消息
public record PostQuestionMqCreated(
    Guid Id,
    string Title,
    string Content,
    DateTime CreateAt,
    string[] Tags
);
