using PostService.Models.Post;

namespace PostService.Repositories.Post;

public interface IPostQuestionRepository
{
    Task<PostQuestion> CreateQuestionAsync(PostQuestion postQuestion);
    Task<PostQuestion?> GetQuestionByIdAsync(string id);
    Task<PostQuestion?> UpdateQuestionAsync(PostQuestion postQuestion, string userId);
    Task<bool> DeleteQuestionAsync(string id, string userId);
    Task<List<PostQuestion>> GetQuestionsAsync(string? tag);
}
