using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostAnswers;
using FluentValidation;
using MediatR;

namespace Application.MediatR.Handlers.PostServer.PostAnswers;

public class UpdatePostAnswerCommandHandler(
    IPostAnswerRepository postAnswerRepository,
    IValidator<UpdatePostAnswerCommand> validator,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdatePostAnswerCommand>
{
    public async Task Handle(UpdatePostAnswerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new CustomValidationException(validationResult);

        var postAnswer = await postAnswerRepository.GetByIdAsync(request.Id);
        if (postAnswer is null)
            throw new NotFoundException();

        postAnswer.UpdateContent(request.Content);

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
