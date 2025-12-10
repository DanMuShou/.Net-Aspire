using MediatR;

namespace Application.MediatR.Commands.PostServer.PostQuestions;

public record PostQuestionDeleteCommand(Guid Id) : IRequest;
