// 端口5010
var builder = DistributedApplication.CreateBuilder(args);

// 端口6001 - 创建持久化容器
var keycloak = builder.AddKeycloak("keycloak", 6001).WithDataVolume("keycloak-data");

// kc-admin

builder.Build().Run();
