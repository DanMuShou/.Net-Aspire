using System.Text.RegularExpressions;
using Application.Common.Queues.Post;
using Application.Common.Typesense;
using SearchService.Models;
using Typesense;

namespace SearchService.Handlers;

/// <summary>
/// 处理问题创建事件的消息处理器，负责将新创建的问题添加到搜索索引中
/// </summary>
/// <param name="client">Typesense客户端，用于与搜索服务进行交互</param>
public class QuestionCreatedHandler(ITypesenseClient client)
{
    /// <summary>
    /// 异步处理问题创建消息，将问题添加到搜索索引中
    /// </summary>
    /// <param name="message">问题创建消息，包含问题的详细信息</param>
    /// <returns>表示异步操作的任务</returns>
    public async Task HandleAsync(PostQuestionMqCreated message)
    {
        // 将创建时间转换为Unix时间戳
        var created = new DateTimeOffset(message.CreateAt).ToUnixTimeSeconds();

        // 创建搜索文档对象
        var doc = new SearchPostQuestion()
        {
            Id = message.Id,
            Title = message.Title,
            Content = StripHtml(message.Content),
            CreatedAt = created,
            Tags = message.Tags.ToArray(),
        };

        // 将文档添加到Typesense的questions集合中
        await client.CreateDocument(TypesenseSchemaName.PostQuestionSchema, doc);

        Console.WriteLine($"Question {message.Id} added to search index.");
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
