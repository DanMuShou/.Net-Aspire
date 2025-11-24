namespace Contracts;

// 传递给问题服务总线的消息
public record QuestionCreated(
    string QuestionId,
    string Title,
    string Content,
    DateTime Created,
    List<string> Tags
);
