using Domain.Entity.PostServer.Post;

namespace Application.Contracts.Repositories.PostServer;

public interface IPostAnswerRepository : IRepository<PostAnswer>
{
    public Task DeleteFromQuestionIdAsync(Guid questionId);
    public Task<List<PostAnswer>> GetByQuestionIdAsync(Guid questionId);
}
