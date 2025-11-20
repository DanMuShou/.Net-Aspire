using System.Text.RegularExpressions;
using Contracts.MessageQueue.Post;
using Contracts.Static.Info;
using SearchService.Models;
using Typesense;

namespace SearchService.MessageHandlers;

/// <summary>
/// 处理回答更新事件的消息处理器，负责更新搜索索引中的回答
/// </summary>
/// <param name="client">Typesense客户端，用于与搜索服务进行交互</param>
public class AnswerUpdatedHandler(ITypesenseClient client)
{
    /// <summary>
    /// 异步处理回答更新消息，更新搜索索引中的回答
    /// </summary>
    /// <param name="message">回答更新消息，包含回答的详细信息</param>
    /// <returns>表示异步操作的任务</returns>
    public async Task HandleAsync(PostAnswerMqUpdated message)
    {
        await client.UpdateDocument(
            TypesenseSchemaName.PostAnswerSchema,
            message.Id,
            new
            {
                message.Content,
                message.IsAccepted,
            }
        );
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
