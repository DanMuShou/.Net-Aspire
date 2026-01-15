using Application.Common.Response;
using Application.DTO.PostServer.PostQuestions;
using MediatR;

namespace Application.MediatR.Commands.PostServer.PostQuestions;

public record PostQuestionUpdateCommand(
    Guid Id,
    string Title,
    string Content,
    List<string> Tags,
    bool HasAcceptedAnswer
) : IRequest<ServiceResponse<PostQuestionDto>>;
