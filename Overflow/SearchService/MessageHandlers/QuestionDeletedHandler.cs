using Contracts;
using Contracts.MessageQueue.Post;
using Contracts.Static.Info;
using SearchService.Models;
using Typesense;

namespace SearchService.MessageHandlers;

public class QuestionDeletedHandler(ITypesenseClient client)
{
    public async Task HandleAsync(PostQuestionMqDeleted message)
    {
        await client.DeleteDocument<SearchQuestion>(
            TypesenseSchemaName.PostQuestionSchema,
            message.Id
        );
    }
}
