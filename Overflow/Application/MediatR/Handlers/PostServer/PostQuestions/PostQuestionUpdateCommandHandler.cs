using Application.Contracts.Persistence;
using Application.Contracts.Queues.Post;
using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using FluentValidation;
using Mapster;
using MediatR;
using Wolverine;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

public class PostQuestionUpdateCommandHandler(
    IPostQuestionRepository repository,
    IPostTagRepository postTagRepository,
    IValidator<PostQuestionUpdateCommand> validator,
    IMessageBus messageBus,
    IUnitOfWork unitOfWork
) : IRequestHandler<PostQuestionUpdateCommand>
{
    public async Task Handle(PostQuestionUpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new CustomValidationException(validationResult);

        if (!await postTagRepository.AreTagListValidAsync(request.Tags))
            throw new CustomValidationException("标签列表无效");

        var postQuestion = await repository.GetByIdAsync(request.Id);
        if (postQuestion is null)
            throw new NotFoundException();

        postQuestion.UpdateTitle(request.Title);
        postQuestion.UpdateContent(request.Content);
        postQuestion.UpdateTagSlugs(request.Tags);
        postQuestion.SetHasAcceptedAnswer(request.HasAcceptedAnswer);
        postQuestion.Update();

        try
        {
            await repository.UpdateAsync(postQuestion);
            await unitOfWork.CommitAsync();
            await messageBus.PublishAsync(postQuestion.Adapt<PostQuestionMqUpdated>());
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}
