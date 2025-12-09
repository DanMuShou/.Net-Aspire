using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostAnswers;
using FluentValidation;
using MediatR;

namespace Application.MediatR.Handlers.PostServer.PostAnswers;

public class DeletePostAnswerCommandHandler(
    IPostAnswerRepository postAnswerRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeletePostAnswerCommand>
{
    public async Task Handle(DeletePostAnswerCommand request, CancellationToken cancellationToken)
    {
        var postAnswer = postAnswerRepository.GetByIdAsync(request.Id).Result;
        if (postAnswer is null)
            throw new NotFoundException();

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
