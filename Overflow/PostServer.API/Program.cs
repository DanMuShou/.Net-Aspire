using Application;
using Domain.Info.Static.Typesense;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Overflow.ServiceDefaults;
using Persistence;
using PostServer.API.Middlewares;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddOpenApi();
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();

builder
    .Services.AddAuthentication()
    .AddKeycloakJwtBearer(
        serviceName: "keyclock", // 服务名称
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

#endregion

#region Host

builder.Host.UseWolverine(options =>
{
    // 自动创建所需的 RabbitMQ 资源.
    options.UseRabbitMqUsingNamedConnection("messaging").AutoProvision();
    options.PublishAllMessages().ToRabbitExchange(TypesenseSchemaName.PostQuestionSchema);
});

#endregion

#region Builder

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<PostServerDbContext>("postDb");

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.UseHttpsRedirection();
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
