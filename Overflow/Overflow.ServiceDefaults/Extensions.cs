using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Overflow.ServiceDefaults;

// 添加常见的 Aspire 服务：服务发现、弹性处理、健康检查和 OpenTelemetry。
// 该工程应当被解决方案中的每个服务项目引用。
// 要了解更多关于如何使用该项目，请参见 https://aka.ms/dotnet/aspire/service-defaults
public static class Extensions
{
    // 健康检查端点路径
    private const string HealthEndpointPath = "/health";

    // 存活状态检查端点路径
    private const string AlivenessEndpointPath = "/alive";

    // 添加默认的服务配置，包括 OpenTelemetry、健康检查和服务发现等
    public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.ConfigureOpenTelemetry();

        builder.AddDefaultHealthChecks();

        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            // 默认开启弹性处理机制
            http.AddStandardResilienceHandler();

            // 默认启用服务发现功能
            http.AddServiceDiscovery();
        });

        // 取消注释以下代码可限制服务发现允许的协议方案。
        // builder.Services.Configure<ServiceDiscoveryOptions>(options =>
        // {
        //     options.AllowedSchemes = ["https"];
        // });

        return builder;
    }

    // 配置 OpenTelemetry 相关设置
    public static TBuilder ConfigureOpenTelemetry<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            // 包含格式化的日志消息
            logging.IncludeFormattedMessage = true;
            // 包含作用域信息
            logging.IncludeScopes = true;
        });

        builder
            .Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                // 添加 ASP.NET Core 指标收集
                metrics
                    .AddAspNetCoreInstrumentation()
                    // 添加 HTTP 客户端指标收集
                    .AddHttpClientInstrumentation()
                    // 添加运行时指标收集
                    .AddRuntimeInstrumentation();
            })
            .WithTracing(tracing =>
            {
                // 添加当前应用程序名称作为跟踪源
                tracing
                    .AddSource(builder.Environment.ApplicationName)
                    // 添加 ASP.NET Core 请求追踪，并排除健康检查请求
                    .AddAspNetCoreInstrumentation(tracing =>
                        tracing.Filter = context =>
                            !context.Request.Path.StartsWithSegments(HealthEndpointPath)
                            && !context.Request.Path.StartsWithSegments(AlivenessEndpointPath)
                    )
                    // 取消注释以下行以启用 gRPC 追踪（需要 OpenTelemetry.Instrumentation.GrpcNetClient 包）
                    //.AddGrpcClientInstrumentation()
                    // 添加 HTTP 客户端追踪
                    .AddHttpClientInstrumentation();
            });

        // 添加 OpenTelemetry 导出器
        builder.AddOpenTelemetryExporters();

        return builder;
    }

    // 添加 OpenTelemetry 的导出器配置
    private static TBuilder AddOpenTelemetryExporters<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        // 判断是否启用了 OTLP 导出器
        var useOtlpExporter = !string.IsNullOrWhiteSpace(
            builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]
        );

        if (useOtlpExporter)
        {
            // 使用 OTLP 导出器
            builder.Services.AddOpenTelemetry().UseOtlpExporter();
        }

        // 取消注释以下代码以启用 Azure Monitor 导出器（需要 Azure.Monitor.OpenTelemetry.AspNetCore 包）
        //if (!string.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
        //{
        //    builder.Services.AddOpenTelemetry()
        //       .UseAzureMonitor();
        //}

        return builder;
    }

    // 添加默认的健康检查配置
    public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder
            .Services.AddHealthChecks()
            // 添加一个默认的存活检查以确保应用启动后能响应流量
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    // 映射默认的终结点（如健康检查）
    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        // 在非开发环境中添加健康检查终结点具有安全风险。
        // 在非开发环境启用这些终结点前，请查看 https://aka.ms/dotnet/aspire/healthchecks 获取详细信息。
        if (app.Environment.IsDevelopment())
        {
            // 所有健康检查都必须通过才能认为应用已准备好接受流量
            app.MapHealthChecks(HealthEndpointPath);

            // 只有标记为 "live" 的健康检查必须通过才能认为应用处于活跃状态
            app.MapHealthChecks(
                AlivenessEndpointPath,
                new HealthCheckOptions { Predicate = r => r.Tags.Contains("live") }
            );
        }

        return app;
    }
}
