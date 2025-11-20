using Contracts.MessageQueue.Post;
using Mapster;
using PostService.Dtos.Post;
using PostService.Models.Post;
using PostService.Repositories.Post;
using Wolverine;

namespace PostService.Services.Post;

public class PostQuestionService(
    IPostQuestionRepository questionRepository,
    IPostTagService tagService,
    IMessageBus bus
) : IPostQuestionService
{
    public async Task<PostQuestionDto> CreateQuestionAsync(
        string userId,
        string userName,
        PostQuestionCreateDto questionDto
    )
    {
        if (!await tagService.AreTagValidAsync(questionDto.Tags))
            throw new ArgumentException("标签无效");

        var question = questionDto.Adapt<PostQuestion>();
        question.AskerId = userId;
        question.AskerDisplayName = userName;
        var result = await questionRepository.CreateQuestionAsync(question);
        await bus.PublishAsync(result.Adapt<PostQuestionMqCreated>());
        return result.Adapt<PostQuestionDto>();
    }

    public async Task<PostQuestionDto?> GetQuestionByIdAsync(string id)
    {
        var result = await questionRepository.GetQuestionByIdAsync(id);
        return result?.Adapt<PostQuestionDto>();
    }

    public async Task<PostQuestionDto?> UpdateQuestionAsync(
        string id,
        string userId,
        PostQuestionUpdateDto questionDto
    )
    {
        if (!await tagService.AreTagValidAsync(questionDto.Tags))
            throw new ArgumentException("标签无效");

        var question = questionDto.Adapt<PostQuestion>();
        question.Id = id;
        var result = await questionRepository.UpdateQuestionAsync(question, userId);
        if (result is not null)
            await bus.PublishAsync(result.Adapt<PostQuestionMqUpdated>());
        return result?.Adapt<PostQuestionDto>();
    }

    public async Task<bool> DeleteQuestionAsync(string id, string userId)
    {
        var result = await questionRepository.DeleteQuestionAsync(id, userId);
        if (result)
            await bus.PublishAsync(new PostQuestionMqDeleted(id));
        return result;
    }

    public async Task<List<PostQuestionDto>> GetQuestionsAsync(string? tag)
    {
        var questions = await questionRepository.GetQuestionsAsync(tag);
        return questions.Adapt<List<PostQuestionDto>>();
    }
}
