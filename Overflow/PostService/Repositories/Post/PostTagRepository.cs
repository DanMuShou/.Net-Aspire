using Microsoft.EntityFrameworkCore;
using PostService.Data;
using PostService.Models.Post;

namespace PostService.Repositories.Post;

public class PostTagRepository(PostDbContext db) : IPostTagRepository
{
    private DbSet<PostTag> Set => db.Set<PostTag>();

    public Task<List<PostTag>> GetTagsAsync()
    {
        return Set.AsNoTracking().ToListAsync();
    }
}
