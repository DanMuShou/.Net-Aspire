using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuestionService.Data;
using QuestionService.Models;

namespace QuestionService.Services;

public class TagService(IMemoryCache cache, QuestionDbContext db)
{
    private const string CachesKey = "Tags";

    public async Task<IEnumerable<Tag>> GetTags()
    {
        return await cache.GetOrCreateAsync(
                CachesKey,
                async entry =>
                {
                    // 过期时长
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2);
                    var tags = await db.Tags.AsNoTracking().ToListAsync();
                    return tags;
                }
            ) ?? [];
    }

    public async Task<bool> AreTagsValidAsync(List<string> slugs)
    {
        var tags = await GetTags();
        var tagSlugs = tags.Select(tag => tag.Slug).ToHashSet(StringComparer.OrdinalIgnoreCase);
        return slugs.All(slug => tagSlugs.Contains(slug));
    }
}
