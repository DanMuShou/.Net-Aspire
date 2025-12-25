namespace Application.Common.Queues.Post;

public record PostQuestionMqUpdated(Guid Id, string Title, string Content, string[] Tags);
