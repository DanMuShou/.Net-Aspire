using PostService.Models.Post;

namespace PostService.Repositories.Post;

public interface IPostTagRepository
{
    Task<List<PostTag>> GetTagsAsync();
}
