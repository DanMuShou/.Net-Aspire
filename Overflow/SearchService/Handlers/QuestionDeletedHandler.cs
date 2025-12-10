using Application.Contracts.Queues.Post;
using Application.Contracts.Typesense;
using SearchService.Models;
using Typesense;

namespace SearchService.Handlers;

public class QuestionDeletedHandler(ITypesenseClient client)
{
    public async Task HandleAsync(PostQuestionMqDeleted message)
    {
        await client.DeleteDocument<SearchPostQuestion>(
            TypesenseSchemaName.PostQuestionSchema,
            message.Id.ToString()
        );
    }
}
