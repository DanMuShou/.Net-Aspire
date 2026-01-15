using Application.Common.Queues.Post;
using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using Application.Static.Messages;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

public class PostAnswerAcceptCommandHandler(
    IPostAnswerRepository postAnswerRepository,
    IPostQuestionRepository postQuestionRepository,
    IMessageBus messageBus,
    IUnitOfWork unitOfWork
) : IRequestHandler<PostAnswerAcceptCommand>
{
    public async Task Handle(PostAnswerAcceptCommand request, CancellationToken cancellationToken)
    {
        var postAnswer = postAnswerRepository.GetByIdAsync(request.PostAnswerId).Result;

        if (postAnswer is null)
            throw new NotFoundException();

        var postQuestion = postQuestionRepository.GetByIdAsync(request.PostQuestionId).Result;
        if (postQuestion is null)
            throw new NotFoundException();

        postQuestion.SetHasAcceptedAnswer(true);
        postAnswer.SetAccepted(true);

        try
        {
            await unitOfWork.CommitAsync();
            await messageBus.PublishAsync(postAnswer.Adapt<PostQuestionMqAnswerAccepted>());
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}
