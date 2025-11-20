using Contracts.Static.Info;
using Microsoft.Extensions.Caching.Memory;
using PostService.Models.Post;
using PostService.Repositories.Post;

namespace PostService.Services.Post;

public class PostTagService(IPostTagRepository tagRepository, IMemoryCache cache) : IPostTagService
{
    private const string CachesKey = nameof(CacheKeyEnum.Tags);

    private async Task<IEnumerable<PostTag>> GetTags()
    {
        return await cache.GetOrCreateAsync(
                CachesKey,
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2);
                    var tags = await tagRepository.GetTagsAsync();
                    return tags;
                }
            ) ?? [];
    }

    public async Task<bool> AreTagValidAsync(List<string> slugs)
    {
        var tags = await GetTags();
        var tagSlugs = tags.Select(tag => tag.Slug).ToHashSet(StringComparer.OrdinalIgnoreCase);
        return slugs.All(slug => tagSlugs.Contains(slug));
    }
}
