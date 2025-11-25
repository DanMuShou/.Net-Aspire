using Mapster;
using PostService.Dtos.Post;
using PostService.Models.Post;
using PostService.Repositories.Post;
using Contracts.MessageQueue.Post;
using Wolverine;

namespace PostService.Services.Post;

public class PostAnswerService(
    IPostAnswerRepository answerRepository,
    IMessageBus bus
) : IPostAnswerService
{
    public async Task<PostAnswerDto> CreateAnswerAsync(
        string userId,
        string userName,
        PostAnswerCreateDto answerDto
    )
    {
        var answer = new PostAnswer
        {
            UserId = userId,
            UserDisplayName = userName,
            PostQuestionId = answerDto.PostQuestionId,
            Content = answerDto.Content,
            CreatedAt = DateTime.UtcNow
        };

        var result = await answerRepository.CreateAnswerAsync(answer);
        
        // 发布回答创建消息
        var message = result.Adapt<PostAnswerMqCreated>();
        await bus.PublishAsync(message);
        
        return result.Adapt<PostAnswerDto>();
    }

    public async Task<PostAnswerDto?> GetAnswerByIdAsync(string id)
    {
        var result = await answerRepository.GetAnswerByIdAsync(id);
        return result?.Adapt<PostAnswerDto>();
    }

    public async Task<PostAnswerDto?> UpdateAnswerAsync(
        string id,
        string userId,
        PostAnswerUpdateDto answerDto
    )
    {
        var answer = new PostAnswer
        {
            Id = id,
            Content = answerDto.Content,
            IsAccepted = answerDto.IsAccepted,
            UserId = userId,
            UserDisplayName = string.Empty, // 这些字段在更新时不重要，但需要满足必需字段要求
            PostQuestionId = string.Empty   // 这些字段在更新时不重要，但需要满足必需字段要求
        };

        var result = await answerRepository.UpdateAnswerAsync(answer, userId);
        
        if (result != null)
        {
            // 发布回答更新消息
            var message = result.Adapt<PostAnswerMqUpdated>();
            await bus.PublishAsync(message);
        }
        
        return result?.Adapt<PostAnswerDto>();
    }

    public async Task<bool> DeleteAnswerAsync(string id, string userId)
    {
        var result = await answerRepository.DeleteAnswerAsync(id, userId);
        
        if (result)
        {
            // 发布回答删除消息
            await bus.PublishAsync(new PostAnswerMqDeleted(id));
        }
        
        return result;
    }

    public async Task<List<PostAnswerDto>> GetAnswersByQuestionIdAsync(string questionId)
    {
        var answers = await answerRepository.GetAnswersByQuestionIdAsync(questionId);
        return answers.Adapt<List<PostAnswerDto>>();
    }
}