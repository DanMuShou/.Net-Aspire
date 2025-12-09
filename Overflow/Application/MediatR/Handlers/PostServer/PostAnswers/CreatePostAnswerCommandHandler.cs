using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostAnswers;
using Domain.Entity.PostServer.Post;
using FluentValidation;
using MediatR;

namespace Application.MediatR.Handlers.PostServer.PostAnswers;

public class CreatePostAnswerCommandHandler(
    IPostAnswerRepository postAnswerRepository,
    IValidator<CreatePostAnswerCommand> validator,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreatePostAnswerCommand, Guid>
{
    public async Task<Guid> Handle(
        CreatePostAnswerCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new CustomValidationException(validationResult);

        var postAnswer = new PostAnswer(request.Content, request.UserId, request.PostQuestionId);

        try
        {
            var result = await postAnswerRepository.AddAsync(postAnswer);
            await unitOfWork.CommitAsync();
            return result.Id;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}
