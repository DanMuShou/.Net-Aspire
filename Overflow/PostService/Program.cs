using Contracts.Static.Info;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Overflow.ServiceDefaults;
using PostService.Configs.Mapper;
using PostService.Data;
using PostService.Repositories.Post;
using PostService.Services.Post;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IPostQuestionRepository, PostQuestionRepository>();
builder.Services.AddScoped<IPostQuestionService, PostQuestionService>();
builder.Services.AddScoped<IPostTagRepository, PostTagRepository>();
builder.Services.AddScoped<IPostTagService, PostTagService>();

MappingConfig.Configure();

#region Keycloak

builder
    .Services.AddAuthentication()
    .AddKeycloakJwtBearer(
        serviceName: "keycloak",
        realm: "overflow",
        options =>
        {
            // 不允许https, 防止Docker签名复杂
            options.RequireHttpsMetadata = false;
            options.Audience = "overflow";
        }
    );

#endregion

#region Postgres

builder.AddNpgsqlDbContext<PostDbContext>("postDb");

#endregion

#region OpenTelemetry

// OpenTelemetry 的分布式追踪功能 防止追踪数据缺失: Aspire 仪表板无法显示服务间的调用链路
builder
    .Services.AddOpenTelemetry()
    .WithTracing(providerBuilder =>
    {
        // 配置追踪提供程序，设置资源构建器，将当前应用名称添加为服务标识
        providerBuilder
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName)
            )
            .AddSource("Wolverine"); // 添加 "Wolverine" 作为追踪源，这样可以追踪通过 Wolverine 消息框架的操作
    });
builder.Host.UseWolverine(options =>
{
    // Wolverine 会自动创建所需的 RabbitMQ 资源，如队列、交换机等
    options.UseRabbitMqUsingNamedConnection("messaging").AutoProvision();
    // 发送消息到 questions 交换机
    options.PublishAllMessages().ToRabbitExchange(TypesenseSchemaName.PostQuestionSchema);
});

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.MapDefaultEndpoints();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<PostDbContext>();
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
