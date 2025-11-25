namespace Contracts.MessageQueue.Post;

// 传递给回答服务总线的消息
public record PostAnswerMqDeleted(
    string Id
);