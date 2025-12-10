using System.Text.RegularExpressions;
using Application.Contracts.Typesense;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Overflow.ServiceDefaults;
using SearchService.Data;
using SearchService.Models;
using Typesense;
using Typesense.Setup;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddOpenApi();

#region Typesense

// 获取 Typesense 服务地址配置项。
// 如果未设置则抛出异常。
var typesenseUri = builder.Configuration.GetValue<string>("services:typesense:typesense:0");
if (string.IsNullOrEmpty(typesenseUri))
    throw new InvalidOperationException("Typesense URL is not set");

// 获取 Typesense API 密钥配置项。
// 如果未设置则抛出异常。
var typesenseApiKey = builder.Configuration.GetValue<string>("typesense-api-key");
if (string.IsNullOrEmpty(typesenseApiKey))
    throw new InvalidOperationException("Typesense API Key is not set");

// 解析 Typesense URI 并注册客户端服务到依赖注入容器中。
var uri = new Uri(typesenseUri);
builder.Services.AddTypesenseClient(configs =>
{
    configs.ApiKey = typesenseApiKey;
    configs.Nodes = [new Node(uri.Host, uri.Port.ToString(), uri.Scheme)];
});

#endregion

#region RabbitMQ

// 配置 OpenTelemetry 分布式追踪功能，并添加 Wolverine 消息处理的追踪源。
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

// 配置 Wolverine 使用 RabbitMQ 进行消息通信，并监听指定队列。
// 同时启用自动资源预配（如交换机和队列）。
builder.Host.UseWolverine(options =>
{
    options.UseRabbitMqUsingNamedConnection("messaging").AutoProvision();
    options.ListenToRabbitQueue(
        "question.search",
        config => config.BindExchange(TypesenseSchemaName.PostQuestionSchema)
    );
});

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapDefaultEndpoints();

app.MapGet(
    "/search",
    async (string query, ITypesenseClient client) =>
    {
        string? tag = null;
        var tagMatch = Regex.Match(query, @"\[(.*?)\]");
        if (tagMatch.Success)
        {
            tag = tagMatch.Groups[1].Value;
            query = query.Replace(tagMatch.Value, string.Empty).Trim();
        }

        var searchParameters = new SearchParameters(query, "title,content");
        if (!string.IsNullOrWhiteSpace(tag))
        {
            searchParameters.FilterBy = $"tags:=[{tag}]";
        }

        try
        {
            var result = await client.Search<SearchPostQuestion>(
                TypesenseSchemaName.PostQuestionSchema,
                searchParameters
            );
            return Results.Ok(result.Hits.Select(hit => hit.Document));
        }
        catch (Exception e)
        {
            return Results.Problem("Typesense search failed", e.Message);
        }
    }
);

// 根据标题相似度进行问题推荐搜索。
app.MapGet(
    "/search/similar-title",
    async (string query, ITypesenseClient client) =>
    {
        var searchParams = new SearchParameters(query, "title");

        try
        {
            var result = await client.Search<SearchPostQuestion>(
                TypesenseSchemaName.PostQuestionSchema,
                searchParams
            );
            return Results.Ok(result.Hits.Select(hit => hit.Document));
        }
        catch (Exception e)
        {
            return Results.Problem("Typesense search failed", e.Message);
        }
    }
);

// 应用启动前确保索引存在，避免首次访问失败。
using var scope = app.Services.CreateScope();
var client = scope.ServiceProvider.GetRequiredService<ITypesenseClient>();
await SearchInitializer.EnsureIndexExists(client);

app.Run();
