using MediatR;

namespace Application.MediatR.Commands.PostServer.PostQuestions;

public record PostAnswerCreateCommand(string Content, string UserId, Guid PostQuestionId)
    : IRequest<Guid>;
