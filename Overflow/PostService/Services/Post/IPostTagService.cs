namespace PostService.Services.Post;

public interface IPostTagService
{
    public Task<bool> AreTagValidAsync(List<string> slugs);
}
