using Microsoft.EntityFrameworkCore;
using QuestionService.Models;

namespace QuestionService.Data;

public class QuestionDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tag>(builder =>
        {
            builder.HasData(
                [
                    new Tag
                    {
                        Id = "dotnet",
                        Name = ".NET",
                        Slug = "dotnet",
                        Description = ".NET是一个免费、开源的开发平台，用于构建多种类型的应用程序",
                    },
                    new Tag
                    {
                        Id = "csharp",
                        Name = "C#",
                        Slug = "csharp",
                        Description = "C#是微软开发的一种面向对象的编程语言，运行于.NET平台上",
                    },
                    new Tag
                    {
                        Id = "aspnet-core",
                        Name = "ASP.NET Core",
                        Slug = "aspnet-core",
                        Description =
                            "ASP.NET Core是一个开源、跨平台的框架，用于构建现代Web应用和服务",
                    },
                    new Tag
                    {
                        Id = "ef-core",
                        Name = "Entity Framework Core",
                        Slug = "ef-core",
                        Description =
                            "Entity Framework Core 是一个轻量级、可扩展且流行的 .NET 对象关系映射 (ORM) 框架",
                    },
                    new Tag
                    {
                        Id = "dotnet-maui",
                        Name = ".NET MAUI",
                        Slug = "dotnet-maui",
                        Description =
                            ".NET MAUI 是一个跨平台框架，用于创建在 Android、iOS、macOS 和 Windows 上运行的本机移动和桌面应用",
                    },
                    new Tag
                    {
                        Id = "blazor",
                        Name = "Blazor",
                        Slug = "blazor",
                        Description =
                            "Blazor 是一个基于 WebAssembly 或 SignalR 的 Web UI 框架，允许使用 C# 而不是 JavaScript 构建交互式 Web 应用",
                    },
                ]
            );
        });
    }
}