using Application;
using Application.Contracts.Typesense;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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

// 替换 AddOpenApi 为完整的 Swagger 配置
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Post Server API", Version = "v1" });

    // 添加 OAuth2 配置
    options.AddSecurityDefinition(
        "oauth2",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(
                        "http://localhost:6001/realms/overflow/protocol/openid-connect/auth"
                    ),
                    TokenUrl = new Uri(
                        "http://localhost:6001/realms/overflow/protocol/openid-connect/token"
                    ),
                    Scopes = new Dictionary<string, string>
                    {
                        { "openid", "OpenID" },
                        { "profile", "Profile" },
                        { "email", "Email" },
                    },
                },
            },
        }
    );

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "oauth2",
                    },
                },
                ["openid", "profile", "email"]
            },
        }
    );
});
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API 文档 v1");
        c.RoutePrefix = string.Empty; // 设置为根路径

        // 配置 OAuth2
        c.OAuthClientId("swagger");
        c.OAuthClientSecret("");
        c.OAuthUsePkce();
        c.OAuthScopeSeparator(" ");
    });
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
