using Application.Common.Queues.Post;
using Application.DTO.PostServer.PostQuestions;
using Application.Extensions.Mapper;
using Domain.Entity.PostServer.Post;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class RegisterApplicationServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(RegisterApplicationServices).Assembly)
        );

        services.AddMapster();
        TypeAdapterConfig.GlobalSettings.AddPostServerMap();

        services.AddValidatorsFromAssembly(typeof(RegisterApplicationServices).Assembly);
    }
}
