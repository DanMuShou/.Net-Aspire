using Typesense;

namespace SearchService.Data;

/// <summary>
/// 搜索服务初始化类，用于确保搜索索引的存在
/// </summary>
public static class SearchInitializer
{
    /// <summary>
    /// 确保指定的索引集合存在，如果不存在则创建新的索引集合
    /// </summary>
    /// <param name="client">Typesense客户端实例，用于与搜索服务进行交互</param>
    /// <returns>表示异步操作的任务</returns>
    public static async Task EnsureIndexExists(ITypesenseClient client)
    {
        const string schemaName = "questions";

        try
        {
            await client.RetrieveCollection(schemaName);
            Console.WriteLine($"Collection {schemaName} already exists.");
            return;
        }
        catch (TypesenseApiNotFoundException e)
        {
            Console.WriteLine($"Collection {schemaName} does not exist.");
            // 创建索引
            var schema = new Schema(
                schemaName,
                [
                    new Field("id", FieldType.String),
                    new Field("title", FieldType.String),
                    new Field("content", FieldType.String),
                    new Field("tags", FieldType.StringArray),
                    new Field("createdAt", FieldType.Int64),
                    new Field("answerCount", FieldType.Int32),
                    new Field("hasAcceptedAnswer", FieldType.Bool),
                ]
            )
            {
                DefaultSortingField = "createdAt",
            };

            await client.CreateCollection(schema);
            Console.WriteLine($"Collection {schemaName} created.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
