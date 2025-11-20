using Contracts.MessageQueue.Post;
using Contracts.Static.Info;
using SearchService.Models;
using Typesense;

namespace SearchService.MessageHandlers;

/// <summary>
/// 处理回答删除事件的消息处理器，负责从搜索索引中删除回答
/// </summary>
/// <param name="client">Typesense客户端，用于与搜索服务进行交互</param>
public class AnswerDeletedHandler(ITypesenseClient client)
{
    /// <summary>
    /// 异步处理回答删除消息，从搜索索引中删除回答
    /// </summary>
    /// <param name="message">回答删除消息，包含回答的ID</param>
    /// <returns>表示异步操作的任务</returns>
    public async Task HandleAsync(PostAnswerMqDeleted message)
    {
        await client.DeleteDocument<SearchAnswer>(TypesenseSchemaName.PostAnswerSchema, message.Id);
    }
}
