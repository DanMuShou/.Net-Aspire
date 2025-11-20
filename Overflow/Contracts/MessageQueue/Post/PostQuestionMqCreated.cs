namespace Contracts.MessageQueue.Post;

// 传递给问题服务总线的消息
public record PostQuestionMqCreated(
    string Id,
    string Title,
    string Content,
    DateTime CreateAt,
    string[] Tags
);
