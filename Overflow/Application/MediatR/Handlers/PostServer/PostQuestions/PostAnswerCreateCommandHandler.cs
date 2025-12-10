using Application.Contracts.Persistence;
using Application.Contracts.Queues.Post;
using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using Domain.Entity.PostServer.Post;
using FluentValidation;
using Mapster;
using MediatR;
using Wolverine;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

public class PostAnswerCreateCommandHandler(
    IPostAnswerRepository postAnswerRepository,
    IPostQuestionRepository postQuestionRepository,
    IValidator<PostAnswerCreateCommand> validator,
    IMessageBus messageBus,
    IUnitOfWork unitOfWork
) : IRequestHandler<PostAnswerCreateCommand, Guid>
{
    public async Task<Guid> Handle(
        PostAnswerCreateCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new CustomValidationException(validationResult);

        var targetPostQuestion = await postQuestionRepository.GetByIdAsync(request.PostQuestionId);
        if (targetPostQuestion is null)
            throw new NotFoundException();

        var postAnswer = new PostAnswer(request.Content, request.UserId, targetPostQuestion.Id);
        targetPostQuestion.AddAnswerCount();

        try
        {
            var result = await postAnswerRepository.AddAsync(postAnswer);
            await unitOfWork.CommitAsync();
            await messageBus.PublishAsync(result.Adapt<PostQuestionMqAnswerCountUpdated>());
            return result.Id;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}
