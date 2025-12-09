using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.PostServer;
using Persistence.UnitsOfWork;

namespace Persistence;

public static class RegisterPersistenceServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkEfCore>();
        services.AddScoped<IPostQuestionRepository, PostQuestionRepository>();
        return services;
    }
}
