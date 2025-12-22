using Aspire.Hosting.Yarp;
using Aspire.Hosting.Yarp.Transforms;
using Microsoft.Extensions.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var compose = builder
    .AddDockerComposeEnvironment("production")
    .WithDashboard(dashboard => dashboard.WithHostPort(8080).WithForwardedHeaders(enabled: true));

#region Keycloak

#pragma warning disable ASPIRECERTIFICATES001 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
var keycloak = builder
    .AddKeycloak("keycloak", 6001)
    .WithDataVolume("keycloak-data")
    .WithoutHttpsCertificate();
//.WithRealmImport(@"../infra/realms")
//.WithEnvironment("KC_HTTP_ENABLED", "true")
//.WithEnvironment("KC_HOSTNAME_STRICT", "false")
//.WithEndpoint(6001, 8080, "keycloak", isExternal: true);
#pragma warning restore ASPIRECERTIFICATES001 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。

#endregion

#region Postgres
var postgres = builder
    .AddPostgres("postgres", port: 5432)
    .WithDataVolume("postgres-data")
    .WithPgAdmin();
var questionDb = postgres.AddDatabase("postDb");
#endregion

#region Typesense

var typesenseApiKey = builder.AddParameter("typesense-api-key", secret: true);

var typesense = builder
    .AddContainer("typesense", "typesense/typesense", "30.0.rca35")
    .WithVolume("typesense-data", "/data")
    .WithEnvironment("TYPESENSE_DATA_DIR", "/data")
    .WithEnvironment("TYPESENSE_API_KEY", typesenseApiKey)
    .WithHttpEndpoint(8108, 8108, name: "typesense");
var typesenseContainer = typesense.GetEndpoint("typesense");

#endregion

#region Rabbitmq

var rabbitmq = builder
    .AddRabbitMQ("messaging")
    .WithDataVolume("rabbitmq-data")
    .WithManagementPlugin(port: 15672); // 作用: 专门为 RabbitMQ 容器启用管理插件界面
#endregion

#region Server

// 添加一个名为 "question-svc" 的项目引用
// 为 QuestionService 项目添加对 keycloak 服务的引用依赖
// 配置应用启动顺序，确保 keycloak 服务完全启动后再启动 QuestionService
var postService = builder
    .AddProject<PostServer>("post-svc")
    .WithReference(keycloak)
    .WithReference(questionDb)
    .WithReference(rabbitmq)
    .WaitFor(keycloak)
    .WaitFor(questionDb)
    .WaitFor(rabbitmq)
    .PublishAsDockerComposeService((resource, service) => service.Name = "post-svc");

var searchService = builder
    .AddProject<SearchService>("search-svc")
    .WithEnvironment("typesense-api-key", typesenseApiKey)
    .WithReference(typesenseContainer) // 如何访问
    .WithReference(rabbitmq)
    .WaitFor(typesense) // 等待服务
    .WaitFor(rabbitmq)
    .PublishAsDockerComposeService((resource, service) => service.Name = "search-svc");

#endregion

#region Yarp

var yarp = builder
    .AddYarp("gateway")
    .WithConfiguration(yarpBuilder =>
    {
        yarpBuilder.AddRoute("/tag/{**catch-all}", postService);
        yarpBuilder
            .AddRoute("/question/{**catch-all}", postService)
            .WithTransformPathRemovePrefix("/question")
            .WithTransformPathPrefix("/api/PostQuestion");
        yarpBuilder.AddRoute("/search/{**catch-all}", searchService);
    })
    .WithEnvironment("ASPNETCORE_URLS", "http://*:8001")
    .WithEndpoint(8001, 8001, scheme: "http", name: "gateway", isExternal: true); // isExternal: true能在docker外访问
#endregion

builder.Build().Run();
