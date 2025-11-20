using System.Text.RegularExpressions;
using Contracts.MessageQueue.Post;
using Contracts.Static.Info;
using SearchService.Models;
using Typesense;

namespace SearchService.MessageHandlers;

/// <summary>
/// 处理回答创建事件的消息处理器，负责将新创建的回答添加到搜索索引中
/// </summary>
/// <param name="client">Typesense客户端，用于与搜索服务进行交互</param>
public class AnswerCreatedHandler(ITypesenseClient client)
{
    /// <summary>
    /// 异步处理回答创建消息，将回答添加到搜索索引中
    /// </summary>
    /// <param name="message">回答创建消息，包含回答的详细信息</param>
    /// <returns>表示异步操作的任务</returns>
    public async Task HandleAsync(PostAnswerMqCreated message)
    {
        // 将创建时间转换为Unix时间戳
        var created = new DateTimeOffset(message.CreatedAt).ToUnixTimeSeconds();

        // 创建搜索文档对象
        var doc = new SearchAnswer()
        {
            Id = message.Id,
            Content = StripHtml(message.Content),
            CreatedAt = created,
            IsAccepted = message.IsAccepted,
            PostQuestionId = message.PostQuestionId,
        };

        // 将文档添加到Typesense的answers集合中
        await client.CreateDocument("answers", doc);

        Console.WriteLine($"Answer {message.Id} added to search index.");
    }

    /// <summary>
    /// 移除HTML标签，只保留纯文本内容
    /// </summary>
    /// <param name="html">包含HTML标签的字符串</param>
    /// <returns>移除HTML标签后的纯文本字符串</returns>
    private static string StripHtml(string html)
    {
        return Regex.Replace(html, "<.*?>", string.Empty);
    }
}
