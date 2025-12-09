using Application.DTO.PostServer.PostQuestions;
using MediatR;

namespace Application.MediatR.Queries.PostServer.PostQuestions;

public record GetPostQuestionQuery(Guid Id) : IRequest<PostQuestionDto>;
