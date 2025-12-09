using Application.Contracts.Repositories.PostServer;
using Domain.Entity.PostServer.Post;

namespace Persistence.Repositories.PostServer;

public class PostTagRepository(PostServerDbContext context)
    : Repository<PostTag>(context),
        IPostTagRepository
{
    private readonly PostServerDbContext _context = context;

    public Task<bool> AreTagListValidAsync(List<string> tagSlugs)
    {
        return Task.FromResult(
            tagSlugs.All(tagSlug =>
                _context
                    .Set<PostTag>()
                    .Any(tag => tag.Slug.Equals(tagSlug, StringComparison.OrdinalIgnoreCase))
            )
        );
    }
}
