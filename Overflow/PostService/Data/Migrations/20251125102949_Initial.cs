using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PostService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostQuestions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    Title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Content = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    AskerId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    AskerDisplayName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ViewCount = table.Column<int>(type: "integer", nullable: false),
                    TagSlugs = table.Column<List<string>>(type: "text[]", nullable: false),
                    HasAcceptedAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    Votes = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostAnswers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    Content = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    UserDisplayName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsAccepted = table.Column<bool>(type: "boolean", nullable: false),
                    QuestionId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostAnswers_PostQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "PostQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "Id", "CreateAt", "Description", "Name", "Slug" },
                values: new object[,]
                {
                    { "aspnet-core", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ASP.NET Core是一个开源、跨平台的框架，用于构建现代Web应用和服务", "ASP.NET Core", "aspnet-core" },
                    { "blazor", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Blazor 是一个基于 WebAssembly 或 SignalR 的 Web UI 框架，允许使用 C# 而不是 JavaScript 构建交互式 Web 应用", "Blazor", "blazor" },
                    { "csharp", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "C#是微软开发的一种面向对象的编程语言，运行于.NET平台上", "C#", "csharp" },
                    { "dotnet", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".NET是一个免费、开源的开发平台，用于构建多种类型的应用程序", ".NET", "dotnet" },
                    { "dotnet-maui", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".NET MAUI 是一个跨平台框架，用于创建在 Android、iOS、macOS 和 Windows 上运行的本机移动和桌面应用", ".NET MAUI", "dotnet-maui" },
                    { "ef-core", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Entity Framework Core 是一个轻量级、可扩展且流行的 .NET 对象关系映射 (ORM) 框架", "Entity Framework Core", "ef-core" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostAnswers_QuestionId",
                table: "PostAnswers",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostAnswers");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "PostQuestions");
        }
    }
}
