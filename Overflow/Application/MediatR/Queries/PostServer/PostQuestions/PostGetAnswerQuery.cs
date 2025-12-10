using Application.DTO.PostServer.PostQuestions;
using MediatR;

namespace Application.MediatR.Queries.PostServer.PostQuestions;

public record PostGetAnswerQuery(Guid Id) : IRequest<PostAnswerDto>;
