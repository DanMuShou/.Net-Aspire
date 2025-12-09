namespace Application.DTO.PostServer.PostQuestions;

public class PostAnswerDto
{
    public Guid Id { get; set; }
    public required string Content { get; set; }
    public required string UserId { get; set; }
    public required string UserDisplayName { get; set; }
    public bool IsAccepted { get; set; }
    public PostQuestionDto? PostQuestionDto { get; set; }
}
