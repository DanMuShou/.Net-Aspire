using MediatR;

namespace Application.MediatR.Commands.PostServer.PostAnswers;

public record CreatePostAnswerCommand(string Content, string UserId, Guid PostQuestionId)
    : IRequest<Guid>;
