using Application;
using Application.Common.Extensions;
using Application.Common.Typesense;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Overflow.ServiceDefaults;
using Persistence;
using PostServer.Middlewares;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<PostServerDbContext>("postDb");

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();

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

await builder.UseWolverineWithRabbitMqAsync(options =>
{
    options.PublishAllMessages().ToRabbitExchange(TypesenseSchemaName.PostQuestionSchema);
    options.ApplicationAssembly = typeof(Program).Assembly;
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
