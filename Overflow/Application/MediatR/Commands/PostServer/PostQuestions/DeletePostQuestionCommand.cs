using MediatR;

namespace Application.MediatR.Commands.PostServer.PostQuestions;

public record DeletePostQuestionCommand(Guid Id) : IRequest;
