using Domain.Entity.PostServer.Post;

namespace Application.Contracts.Repositories.PostServer;

public interface IPostTagRepository : IRepository<PostTag>
{
    public Task<bool> AreTagListValidAsync(List<string> tagSlugs);
}
