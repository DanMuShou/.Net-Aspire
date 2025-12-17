using Domain.Entity.PostServer.Post;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configs;

public static class PostServerConfigs
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostQuestion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AskerId).HasMaxLength(36);
            entity.Property(e => e.Title).HasMaxLength(300);
            entity.Property(e => e.Content).HasMaxLength(5000);
        });

        modelBuilder.Entity<PostAnswer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId).HasMaxLength(36);
            entity.Property(e => e.Content).HasMaxLength(5000);
        });

        modelBuilder.Entity<PostTag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Slug).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(1000);

            entity.HasData(
                new
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = ".NET Core",
                    Slug = "dotnet-core",
                    Description = "用于构建跨平台应用程序的开源框架",
                    CreateAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                },
                new
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "ASP.NET Core",
                    Slug = "aspnet-core",
                    Description = "用于构建现代Web应用程序和服务的跨平台框架",
                    CreateAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                },
                new
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "C#",
                    Slug = "csharp",
                    Description = "面向对象的编程语言，专为.NET平台设计",
                    CreateAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                },
                new
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Name = "Entity Framework Core",
                    Slug = "ef-core",
                    Description = ".NET的对象关系映射器",
                    CreateAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                },
                new
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Name = "Blazor",
                    Slug = "blazor",
                    Description = "用于构建交互式Web UI的.NET框架",
                    CreateAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                },
                new
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Name = "MAUI",
                    Slug = "maui",
                    Description = ".NET多平台应用用户界面框架",
                    CreateAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                },
                new
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    Name = "SignalR",
                    Slug = "signalr",
                    Description = "用于实时Web功能的库",
                    CreateAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                },
                new
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    Name = "gRPC",
                    Slug = "grpc",
                    Description = "高性能RPC框架",
                    CreateAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                },
                new
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Name = "Microservices",
                    Slug = "microservices",
                    Description = "微服务架构模式",
                    CreateAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                },
                new
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Name = "Docker",
                    Slug = "docker",
                    Description = "容器化平台，用于打包和部署应用程序",
                    CreateAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                }
            );
        });
    }
}
