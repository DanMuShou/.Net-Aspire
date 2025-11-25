namespace Contracts.MessageQueue.Post;

public record PostQuestionMqUpdated(string Id, string Title, string Content, string[] Tags);
