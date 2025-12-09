namespace Application.DTO.PostServer.PostQuestions;

public class PostQuestionDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string AskerId { get; set; }
    public required string AskerDisplayName { get; set; }
    public required List<string> Tags { get; set; }
    public bool HasAcceptedAnswer { get; set; }
    public int Votes { get; set; }
    public List<PostAnswerDto> Answers { get; set; } = [];
}
