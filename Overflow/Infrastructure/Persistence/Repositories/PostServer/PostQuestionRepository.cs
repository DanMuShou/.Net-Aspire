using Application.Contracts.Repositories.PostServer;
using Domain.Entity.PostServer.Post;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.PostServer;

public class PostQuestionRepository(PostServerDbContext context)
    : Repository<PostQuestion>(context),
        IPostQuestionRepository
{
    private readonly PostServerDbContext _context = context;

    public async Task<List<PostQuestion>> GetAllByTag(string? tag)
    {
        if (tag is not null)
            return await _context
                .Set<PostQuestion>()
                .AsNoTracking()
                .Where(q => q.TagSlugs.Contains(tag))
                .ToListAsync();
        return await _context.Set<PostQuestion>().AsNoTracking().ToListAsync();
    }
}
