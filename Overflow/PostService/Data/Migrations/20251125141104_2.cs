using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostService.Data.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostAnswers_PostQuestions_QuestionId",
                table: "PostAnswers");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "PostAnswers",
                newName: "PostQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_PostAnswers_QuestionId",
                table: "PostAnswers",
                newName: "IX_PostAnswers_PostQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostAnswers_PostQuestions_PostQuestionId",
                table: "PostAnswers",
                column: "PostQuestionId",
                principalTable: "PostQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostAnswers_PostQuestions_PostQuestionId",
                table: "PostAnswers");

            migrationBuilder.RenameColumn(
                name: "PostQuestionId",
                table: "PostAnswers",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_PostAnswers_PostQuestionId",
                table: "PostAnswers",
                newName: "IX_PostAnswers_QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostAnswers_PostQuestions_QuestionId",
                table: "PostAnswers",
                column: "QuestionId",
                principalTable: "PostQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
