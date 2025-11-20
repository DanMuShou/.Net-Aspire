using System.Text.RegularExpressions;
using Contracts;
using Contracts.MessageQueue.Post;
using Contracts.Static.Info;
using Typesense;

namespace SearchService.MessageHandlers;

public class QuestionUpdatedHandler(ITypesenseClient client)
{
    public async Task HandleAsync(PostQuestionMqUpdated message)
    {
        // 使用匿名更新防止进行值覆盖
        await client.UpdateDocument(
            TypesenseSchemaName.PostQuestionSchema,
            message.Id,
            new
            {
                message.Title,
                Content = StripHtml(message.Content),
                Tags = message.Tags.ToArray(),
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
