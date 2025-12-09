using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Application.DTO.PostServer.PostQuestions;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

public class UpdatePostQuestionCommandHandler(
    IPostQuestionRepository repository,
    IPostTagRepository postTagRepository,
    IValidator<UpdatePostQuestionCommand> validator,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdatePostQuestionCommand>
{
    public async Task Handle(UpdatePostQuestionCommand request, CancellationToken cancellationToken)
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
        postQuestion.UpdateHasAcceptedAnswer(request.HasAcceptedAnswer);
        postQuestion.Update();

        try
        {
            await repository.UpdateAsync(postQuestion);
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}
