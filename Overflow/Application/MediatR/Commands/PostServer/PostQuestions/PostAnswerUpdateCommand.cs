using MediatR;

namespace Application.MediatR.Commands.PostServer.PostQuestions;

public record PostAnswerUpdateCommand(Guid Id, Guid PostQuestionId, string Content) : IRequest;
