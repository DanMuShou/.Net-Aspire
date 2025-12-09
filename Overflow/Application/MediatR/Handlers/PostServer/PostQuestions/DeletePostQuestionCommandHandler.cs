using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using MediatR;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

public class DeletePostQuestionCommandHandler(
    IPostQuestionRepository repository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeletePostQuestionCommand>
{
    public async Task Handle(DeletePostQuestionCommand request, CancellationToken cancellationToken)
    {
        var postQuestion = await repository.GetByIdAsync(request.Id);
        if (postQuestion is null)
            throw new NotFoundException();

        try
        {
            await repository.DeleteAsync(postQuestion);
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}
