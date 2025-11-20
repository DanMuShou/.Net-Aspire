using PostService.Dtos.Post;
using PostService.Models.Post;

namespace PostService.Services.Post;

public interface IPostAnswerService
{
    Task<PostAnswerDto> CreateAnswerAsync(
        string userId,
        string userName,
        PostAnswerCreateDto answerDto
    );
    Task<PostAnswerDto?> GetAnswerByIdAsync(string id);
    Task<PostAnswerDto?> UpdateAnswerAsync(
        string id,
        string userId,
        PostAnswerUpdateDto answerDto
    );
    Task<bool> DeleteAnswerAsync(string id, string userId);
    Task<List<PostAnswerDto>> GetAnswersByQuestionIdAsync(string questionId);
}