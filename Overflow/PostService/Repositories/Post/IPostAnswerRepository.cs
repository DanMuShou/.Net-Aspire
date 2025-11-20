using PostService.Models.Post;

namespace PostService.Repositories.Post;

public interface IPostAnswerRepository
{
    Task<PostAnswer> CreateAnswerAsync(PostAnswer postAnswer);
    Task<PostAnswer?> GetAnswerByIdAsync(string id);
    Task<PostAnswer?> UpdateAnswerAsync(PostAnswer postAnswer, string userId);
    Task<bool> DeleteAnswerAsync(string id, string userId);
    Task<List<PostAnswer>> GetAnswersByQuestionIdAsync(string questionId);
}