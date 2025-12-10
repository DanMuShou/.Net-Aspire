using MediatR;

namespace Application.MediatR.Commands.PostServer.PostQuestions;

public record PostAnswerDeleteCommand(Guid Id) : IRequest;
