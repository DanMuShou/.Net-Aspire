using Application.Contracts.Repositories.PostServer;
using Application.DTO.PostServer.PostQuestions;
using Application.Exceptions;
using Application.MediatR.Queries.PostServer.PostQuestions;
using Mapster;
using MediatR;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

public class GetPostQuestionQueryHandler(IPostQuestionRepository repository)
    : IRequestHandler<GetPostQuestionQuery, PostQuestionDto>
{
    public async Task<PostQuestionDto> Handle(
        GetPostQuestionQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await repository.GetByIdAsync(request.Id);
        return result is null ? throw new NotFoundException() : result.Adapt<PostQuestionDto>();
    }
}
