using MediatR;

namespace Application.MediatR.Commands.PostServer.PostQuestions;

public record PostAnswerAcceptCommand(Guid PostAnswerId, Guid PostQuestionId) : IRequest;
