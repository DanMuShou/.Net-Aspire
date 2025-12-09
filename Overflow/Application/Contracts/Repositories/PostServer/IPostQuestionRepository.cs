using Domain.Entity.PostServer.Post;

namespace Application.Contracts.Repositories.PostServer;

public interface IPostQuestionRepository : IRepository<PostQuestion>
{
    public Task<List<PostQuestion>> GetAllByTag(string? tag);
}
