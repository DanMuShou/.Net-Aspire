using System.Text.RegularExpressions;
using Contracts;
using SearchService.Models;
using Typesense;

namespace SearchService.MessageHandlers;

public class QuestionUpdatedHandler(ITypesenseClient client)
{
    public async Task HandleAsync(QuestionUpdated message)
    {
        var doc = new SearchQuestion()
        {
            Id = message.QuestionId,
            Title = message.Title,
            Content = StripHtml(message.Content),
            Tags = message.Tags.ToArray(),
        };
        await client.UpdateDocument("questions", doc.Id, doc);
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
