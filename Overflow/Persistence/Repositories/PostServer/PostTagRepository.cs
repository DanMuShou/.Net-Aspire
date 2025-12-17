using Application.Contracts.Repositories.PostServer;
using Domain.Entity.PostServer.Post;

namespace Persistence.Repositories.PostServer;

public class PostTagRepository(PostServerDbContext context)
    : Repository<PostTag>(context),
        IPostTagRepository
{
    private readonly PostServerDbContext _context = context;

    public Task<List<string>> AreTagListValidWithMissingTagsAsync(List<string> tagSlugs)
    {
        var validTagSlugs = _context
            .Set<PostTag>()
            .Where(tag => tagSlugs.Contains(tag.Slug))
            .Select(tag => tag.Slug)
            .ToList();
        var missingTagSlugs = tagSlugs.Except(validTagSlugs).ToList();
        return Task.FromResult(missingTagSlugs);
    }
}
