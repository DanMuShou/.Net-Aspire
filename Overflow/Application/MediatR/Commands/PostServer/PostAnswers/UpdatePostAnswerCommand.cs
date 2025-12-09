using MediatR;

namespace Application.MediatR.Commands.PostServer.PostAnswers;

public record UpdatePostAnswerCommand(Guid Id, string Content) : IRequest;
