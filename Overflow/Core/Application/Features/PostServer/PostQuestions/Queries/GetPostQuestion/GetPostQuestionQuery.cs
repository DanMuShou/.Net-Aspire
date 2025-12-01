using MediatR;

namespace Application.Features.PostServer.PostQuestions.Queries.GetPostQuestion;

public class GetPostQuestionQuery : IRequest<PostQuestionDto>
{
    public required Guid Id { get; init; }
}
