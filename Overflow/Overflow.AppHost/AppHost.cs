using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// 端口6001 - 创建持久化容器
var keycloak = builder.AddKeycloak("keycloak", 6001).WithDataVolume("keycloak-data");

// 添加一个名为 "question-svc" 的项目引用
// 为 QuestionService 项目添加对 keycloak 服务的引用依赖
// 配置应用启动顺序，确保 keycloak 服务完全启动后再启动 QuestionService
var questionService = builder
    .AddProject<QuestionService>("question-svc")
    .WithReference(keycloak)
    .WaitFor(keycloak);

builder.Build().Run();
