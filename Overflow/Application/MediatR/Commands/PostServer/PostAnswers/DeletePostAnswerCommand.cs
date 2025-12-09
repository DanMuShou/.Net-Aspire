using MediatR;

namespace Application.MediatR.Commands.PostServer.PostAnswers;

public record DeletePostAnswerCommand(Guid Id) : IRequest;
