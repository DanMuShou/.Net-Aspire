using Application.DTO.PostServer.PostQuestions;
using MediatR;

namespace Application.MediatR.Queries.PostServer.PostQuestions;

public record PostQuestionGetListQuery(string? Tag) : IRequest<List<PostQuestionDto>>;
