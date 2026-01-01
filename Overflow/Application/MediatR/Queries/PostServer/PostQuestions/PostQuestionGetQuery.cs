using Application.Common.Response;
using Application.DTO.PostServer.PostQuestions;
using MediatR;

namespace Application.MediatR.Queries.PostServer.PostQuestions;

public record PostQuestionGetQuery(Guid Id) : IRequest<ServiceResponse<PostQuestionDto>>;
