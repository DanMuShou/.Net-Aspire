using Application.Common.Typesense;
using Typesense;

namespace SearchService.Data;

/// <summary>
/// 搜索服务初始化类，用于确保搜索索引的存在
/// </summary>
public static class SearchInitializer
{
    private static async Task EnsureIndexExists(
        ITypesenseClient client,
        string schemaName,
        Schema schema
    )
    {
        try
        {
            await client.RetrieveCollection(schemaName);
            Console.WriteLine($"Collection {schemaName} already exists.");
        }
        catch (TypesenseApiNotFoundException)
        {
            Console.WriteLine($"Collection {schemaName} does not exist.");
            await client.CreateCollection(schema);
            Console.WriteLine($"Collection {schemaName} created.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task EnsureIndexExists(ITypesenseClient client)
    {
        await EnsureIndexExists(
            client,
            TypesenseSchemaName.PostQuestionSchema,
            new Schema(
                TypesenseSchemaName.PostQuestionSchema,
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
            }
        );
    }
}
