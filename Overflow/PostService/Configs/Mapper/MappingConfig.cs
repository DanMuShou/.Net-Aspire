using Contracts.MessageQueue.Post;
using Mapster;
using PostService.Dtos.Post;
using PostService.Models.Post;

namespace PostService.Configs.Mapper;

public static class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<PostQuestion, PostQuestionCreateDto>
            .NewConfig()
            .TwoWays()
            .Map(tar => tar.Tags, sou => sou.TagSlugs);
        TypeAdapterConfig<PostQuestion, PostQuestionUpdateDto>
            .NewConfig()
            .TwoWays()
            .Map(tar => tar.Tags, sou => sou.TagSlugs);
        TypeAdapterConfig<PostQuestion, PostQuestionDto>.NewConfig().TwoWays();
        TypeAdapterConfig<PostQuestion, PostQuestionMqCreated>
            .NewConfig()
            .Map(tar => tar.Tags, sou => sou.TagSlugs);
        TypeAdapterConfig<PostQuestion, PostQuestionMqUpdated>
            .NewConfig()
            .Map(tar => tar.Tags, sou => sou.TagSlugs)
            .Map(tar => tar.Id, sou => sou.Id);
        TypeAdapterConfig<PostQuestion, PostQuestionMqDeleted>.NewConfig();

        TypeAdapterConfig<PostAnswer, PostAnswerDto>.NewConfig().TwoWays();
        TypeAdapterConfig<PostAnswer, PostAnswerCreateDto>.NewConfig().TwoWays();
        TypeAdapterConfig<PostAnswer, PostAnswerUpdateDto>.NewConfig().TwoWays();
    }
}
