using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using FluentValidation;
using MediatR;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

public class PostAnswerUpdateCommandHandler(
    IPostAnswerRepository postAnswerRepository,
    IValidator<PostAnswerUpdateCommand> validator,
    IUnitOfWork unitOfWork
) : IRequestHandler<PostAnswerUpdateCommand>
{
    public async Task Handle(PostAnswerUpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new CustomValidationException(validationResult);

        var postAnswer = await postAnswerRepository.GetByIdAsync(request.Id);
        if (postAnswer is null)
            throw new NotFoundException();

        if (request.PostQuestionId != postAnswer.PostQuestionId)
            throw new DataMismatchException();

        postAnswer.UpdateContent(request.Content);
        postAnswer.Update();

        try
        {
            await postAnswerRepository.UpdateAsync(postAnswer);
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}
