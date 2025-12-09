using Application.DTO.PostServer.PostQuestions;
using Domain.Entity.PostServer.Post;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class RegisterApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(RegisterApplicationServices).Assembly);
        services.AddMapster();

        TypeAdapterConfig<PostQuestion, PostQuestionDto>.NewConfig();

        return services;
    }
}
