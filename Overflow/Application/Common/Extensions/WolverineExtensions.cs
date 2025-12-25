using System.Net.Sockets;
using Application.Common.Typesense;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using Wolverine;
using Wolverine.RabbitMQ;

namespace Application.Common.Extensions;

public static class WolverineExtensions
{
    public static async Task UseWolverineWithRabbitMqAsync(
        this IHostApplicationBuilder builder,
        Action<WolverineOptions> configureMessaging
    )
    {
        {
            var retryPolicy = Policy
                .Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetryAsync(
                    5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        Console.WriteLine(
                            $"重试计数: {retryCount}, 重试时间: {timeSpan}"
                                + Environment.NewLine
                                + $"异常: {exception}"
                                + Environment.NewLine
                                + $"上下文: {context}"
                        );
                    }
                );

            await retryPolicy.ExecuteAsync(async () =>
            {
                var endpoint =
                    builder.Configuration.GetConnectionString("messaging")
                    ?? throw new InvalidOperationException("RabbitMQ 链接字符串未找到");

                var factory = new ConnectionFactory { Uri = new Uri(endpoint) };
                await using var connection = await factory.CreateConnectionAsync();
            });

            // 配置 OpenTelemetry 分布式追踪功能，并添加 Wolverine 消息处理的追踪源。
            builder
                .Services.AddOpenTelemetry()
                .WithTracing(providerBuilder =>
                {
                    providerBuilder
                        .SetResourceBuilder(
                            ResourceBuilder
                                .CreateDefault()
                                .AddService(builder.Environment.ApplicationName)
                        )
                        .AddSource("Wolverine");
                });

            // 配置 Wolverine 使用 RabbitMQ 进行消息通信，并监听指定队列。
            // 同时启用自动资源预配（如交换机和队列）。
            builder.UseWolverine(options =>
            {
                options
                    .UseRabbitMqUsingNamedConnection("messaging")
                    .AutoProvision()
                    .DeclareExchange(TypesenseSchemaName.PostQuestionSchema);
                configureMessaging(options);
            });
        }
    }
}
