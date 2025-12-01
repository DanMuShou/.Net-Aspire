using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Mapster;
using MediatR;

namespace Application.Features.PostServer.PostQuestions.Queries.GetPostQuestion;

public class GetPostQuestionQueryHandler(IPostQuestionRepository repository)
    : IRequestHandler<GetPostQuestionQuery, PostQuestionDto>
{
    public async Task<PostQuestionDto> Handle(
        GetPostQuestionQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await repository.GetByIdAsync(request.Id);
        if (result is null)
            throw new NotFoundException();

        return result.Adapt<PostQuestionDto>();
    }
}
