using Projects;

var builder = DistributedApplication.CreateBuilder(args);

#region Keycloak
var keycloak = builder.AddKeycloak("keycloak", 6001).WithDataVolume("keycloak-data");
#endregion

#region Postgres
var postgres = builder
    .AddPostgres("postgres", port: 5432)
    .WithDataVolume("postgres-data")
    .WithPgAdmin();
var questionDb = postgres.AddDatabase("postDb");
#endregion

#region typesense
var typesenseKey = builder.AddParameter("typesense-api-key", secret: true);
var typesense = builder
    .AddContainer("typesense", "typesense/typesense", "29.0")
    .WithArgs("--data-dir", "/data", "--api-key", typesenseKey, "--enable-cors")
    .WithVolume("typesense-data", "/data")
    .WithHttpEndpoint(8108, 8108, "typesense");
var typesenseContainer = typesense.GetEndpoint("typesense");
#endregion

#region rabbitmq
var rabbitmq = builder
    .AddRabbitMQ("messaging")
    .WithDataVolume("rabbitmq-data")
    .WithManagementPlugin(15672); // 作用: 专门为 RabbitMQ 容器启用管理插件界面
#endregion

// 添加一个名为 "question-svc" 的项目引用
// 为 QuestionService 项目添加对 keycloak 服务的引用依赖
// 配置应用启动顺序，确保 keycloak 服务完全启动后再启动 QuestionService
var postService = builder
    .AddProject<PostServer_API>("post-svc")
    .WithReference(keycloak)
    .WithReference(questionDb)
    .WithReference(rabbitmq)
    .WaitFor(keycloak)
    .WaitFor(questionDb)
    .WaitFor(rabbitmq);

// var searchService = builder
//     .AddProject<SearchService>("search-svc")
//     .WithEnvironment("typesense-api-key", typesenseKey)
//     .WithReference(typesenseContainer) // 如何访问
//     .WithReference(rabbitmq)
//     .WaitFor(typesense) // 等待服务
//     .WaitFor(rabbitmq);

builder.Build().Run();
