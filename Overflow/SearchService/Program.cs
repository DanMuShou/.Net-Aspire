using System.Text.RegularExpressions;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SearchService.Data;
using SearchService.Models;
using Typesense;
using Typesense.Setup;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddServiceDefaults();

var typesenseUri = builder.Configuration.GetValue<string>("services:typesense:typesense:0");
if (string.IsNullOrEmpty(typesenseUri))
    throw new InvalidOperationException("Typesense URL is not set");

var typesenseApiKey = builder.Configuration.GetValue<string>("typesense-api-key");
if (string.IsNullOrEmpty(typesenseApiKey))
    throw new InvalidOperationException("Typesense API Key is not set");

var uri = new Uri(typesenseUri);
builder.Services.AddTypesenseClient(configs =>
{
    configs.ApiKey = typesenseApiKey;
    configs.Nodes = [new Node(uri.Host, uri.Port.ToString(), uri.Scheme)];
});

builder
    .Services.AddOpenTelemetry() // OpenTelemetry 的分布式追踪功能
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
    // 作用: 配置当前服务从 RabbitMQ 队列 question.search 监听消息，并绑定到 questions 交换机
    options.ListenToRabbitQueue(
        "question.search",
        config =>
        {
            config.BindExchange("questions");
        }
    );
});

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
        // [aspire]something
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
            var result = await client.Search<SearchQuestion>("questions", searchParameters);
            return Results.Ok(result.Hits.Select(hit => hit.Document));
        }
        catch (Exception e)
        {
            return Results.Problem("Typesense search failed", e.Message);
        }
    }
);

using var scope = app.Services.CreateScope();
var client = scope.ServiceProvider.GetRequiredService<ITypesenseClient>();
await SearchInitializer.EnsureIndexExists(client);

app.Run();
