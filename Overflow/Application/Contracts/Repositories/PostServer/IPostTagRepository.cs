using Domain.Entity.PostServer.Post;

namespace Application.Contracts.Repositories.PostServer;

public interface IPostTagRepository : IRepository<PostTag>
{
    public Task<List<string>> AreTagListValidWithMissingTagsAsync(List<string> tagSlugs);
}
