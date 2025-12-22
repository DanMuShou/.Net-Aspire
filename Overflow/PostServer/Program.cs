using Application;
using Application.Contracts.Typesense;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Persistence;
using PostServer.Middlewares;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<PostServerDbContext>("postDb");

builder.Services.AddControllers();

// 替换 AddOpenApi 为完整的 Swagger 配置
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();

builder
    .Services.AddAuthentication()
    .AddKeycloakJwtBearer(
        serviceName: "keycloak", // 服务名称
        realm: "overflow", // 域名称
        options =>
        {
            options.RequireHttpsMetadata = false;
            options.Audience = "overflow";
        }
    );

builder
    .Services.AddOpenTelemetry()
    .WithTracing(providerBuilder =>
    {
        providerBuilder
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName)
            )
            .AddSource("Wolverine");
    });

builder.Host.UseWolverine(options =>
{
    // 自动创建所需的 RabbitMQ 资源.
    options.UseRabbitMqUsingNamedConnection("messaging").AutoProvision();
    options.PublishAllMessages().ToRabbitExchange(TypesenseSchemaName.PostQuestionSchema);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.UseErrorHandleMiddleware();

using var scope = app.Services.CreateScope();

try
{
    var context = scope.ServiceProvider.GetRequiredService<PostServerDbContext>();
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
