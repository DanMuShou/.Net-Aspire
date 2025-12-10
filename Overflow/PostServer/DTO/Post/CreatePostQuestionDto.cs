namespace PostServer.DTO.Post;

public record CreatePostQuestionDto(string Title, string Content, List<string> Tags);
