using Application.Contracts.Persistence;
using Application.Contracts.Queues.Post;
using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using Mapster;
using MediatR;
using Wolverine;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

public class PostQuestionDeleteCommandHandler(
    IPostQuestionRepository repository,
    IPostAnswerRepository postAnswerRepository,
    IMessageBus messageBus,
    IUnitOfWork unitOfWork
) : IRequestHandler<PostQuestionDeleteCommand>
{
    public async Task Handle(PostQuestionDeleteCommand request, CancellationToken cancellationToken)
    {
        var postQuestion = await repository.GetByIdAsync(request.Id);
        if (postQuestion is null)
            throw new NotFoundException();

        try
        {
            await repository.DeleteAsync(postQuestion);
            await postAnswerRepository.DeleteFromQuestionIdAsync(postQuestion.Id);
            await unitOfWork.CommitAsync();
            await messageBus.PublishAsync(postQuestion.Adapt<PostQuestionMqDeleted>());
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}
