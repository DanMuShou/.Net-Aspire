using Application.Contracts.Queues.Post;
using Application.DTO.PostServer.PostQuestions;
using Domain.Entity.PostServer.Post;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class RegisterApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(RegisterApplicationServices).Assembly)
        );
        services.AddMapster();
        services.AddValidatorsFromAssembly(typeof(RegisterApplicationServices).Assembly);

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

        return services;
    }
}
