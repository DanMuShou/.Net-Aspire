using Application.Contracts.Repositories.PostServer;
using Application.DTO.PostServer.PostQuestions;
using Application.MediatR.Queries.PostServer.PostQuestions;
using Mapster;
using MediatR;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

public class PostQuestionGetListQueryHandler(IPostQuestionRepository repository)
    : IRequestHandler<PostQuestionGetListQuery, List<PostQuestionDto>>
{
    public async Task<List<PostQuestionDto>> Handle(
        PostQuestionGetListQuery request,
        CancellationToken cancellationToken
    )
    {
        var questionList = await repository.GetAllByTag(request.Tag);
        return questionList.Adapt<List<PostQuestionDto>>();
    }
}
