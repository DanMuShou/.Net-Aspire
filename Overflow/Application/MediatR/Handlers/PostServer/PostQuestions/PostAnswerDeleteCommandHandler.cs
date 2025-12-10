using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using MediatR;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

public class PostAnswerDeleteCommandHandler(
    IPostAnswerRepository postAnswerRepository,
    IPostQuestionRepository postQuestionRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<PostAnswerDeleteCommand>
{
    public async Task Handle(PostAnswerDeleteCommand request, CancellationToken cancellationToken)
    {
        var postAnswer = postAnswerRepository.GetByIdAsync(request.Id).Result;
        if (postAnswer is null)
            throw new NotFoundException();

        var postQuestion = postQuestionRepository.GetByIdAsync(postAnswer.PostQuestionId).Result;
        if (postQuestion is null)
            throw new NotFoundException();

        postQuestion.RemoveAnswerCount();

        try
        {
            await postAnswerRepository.DeleteAsync(postAnswer);
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}
