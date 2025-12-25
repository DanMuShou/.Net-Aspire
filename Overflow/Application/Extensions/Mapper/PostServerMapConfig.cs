using Application.Common.Queues.Post;
using Application.DTO.PostServer.PostQuestions;
using Domain.Entity.PostServer.Post;
using Mapster;

namespace Application.Extensions.Mapper;

public static class PostServerMapConfig
{
    public static void AddPostServerMap(this TypeAdapterConfig _)
    {
        TypeAdapterConfig<PostQuestion, PostQuestionDto>
            .NewConfig()
            .Map(tar => tar.Tags, sou => sou.TagSlugs);
        TypeAdapterConfig<PostQuestion, PostQuestionMqCreated>
            .NewConfig()
            .Map(tar => tar.Tags, sou => sou.TagSlugs);
        TypeAdapterConfig<PostQuestion, PostQuestionMqUpdated>
            .NewConfig()
            .Map(tar => tar.Tags, sou => sou.TagSlugs);
        TypeAdapterConfig<PostQuestion, PostQuestionMqDeleted>.NewConfig();
        TypeAdapterConfig<PostQuestion, PostQuestionMqAnswerCountUpdated>.NewConfig();
        TypeAdapterConfig<PostQuestion, PostQuestionMqAnswerAccepted>.NewConfig();

        TypeAdapterConfig<PostAnswer, PostAnswerDto>.NewConfig();
    }
}
