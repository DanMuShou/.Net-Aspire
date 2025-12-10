namespace PostServer.DTO.Post;

public record UpdatePostQuestionDto(
    string Title,
    string Content,
    List<string> Tags,
    bool HasAcceptedAnswer
);
