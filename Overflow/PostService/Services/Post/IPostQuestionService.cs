using PostService.Dtos.Post;

namespace PostService.Services.Post;

public interface IPostQuestionService
{
    Task<PostQuestionDto> CreateQuestionAsync(
        string userId,
        string userName,
        PostQuestionCreateDto questionDto
    );
    Task<PostQuestionDto?> GetQuestionByIdAsync(string id);
    Task<PostQuestionDto?> UpdateQuestionAsync(
        string id,
        string userId,
        PostQuestionUpdateDto questionDto
    );
    Task<bool> DeleteQuestionAsync(string id, string userId);
    Task<List<PostQuestionDto>> GetQuestionsAsync(string? tag);
}
