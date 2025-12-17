using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Application.DTO.PostServer.PostQuestions;
using Application.Exceptions;
using Application.MediatR.Queries.PostServer.PostQuestions;
using Mapster;
using MediatR;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

public class PostQuestionGetQueryHandler(
    IPostQuestionRepository repository,
    IPostAnswerRepository postAnswerRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<PostQuestionGetQuery, PostQuestionDto>
{
    public async Task<PostQuestionDto> Handle(
        PostQuestionGetQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await repository.GetByIdAsync(request.Id);
        if (result is null)
            throw new NotFoundException();

        result.AddView();
        var response = result.Adapt<PostQuestionDto>();
        response.Answers = postAnswerRepository
            .GetByQuestionIdAsync(request.Id)
            .Result.Adapt<List<PostAnswerDto>>();

        try
        {
            await unitOfWork.CommitAsync();
            return response;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}
