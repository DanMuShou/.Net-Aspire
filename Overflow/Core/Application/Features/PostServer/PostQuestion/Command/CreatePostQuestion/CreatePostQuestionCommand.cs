namespace Application.Features.PostServer.PostQuestion.Command.CreatePostQuestion;

public record CreatePostQuestionCommand(
    string Title,
    string Content,
    string AskedByUserId,
    string AskedByUserDisplayName,
    List<string> TagSlugs
);
