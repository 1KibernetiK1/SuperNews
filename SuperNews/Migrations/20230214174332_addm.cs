using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperNews.Migrations
{
    public partial class addm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Rubrics_ArticleRubricRubricId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Comments_CommentId",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleRubricRubricId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleRubricRubricId",
                table: "Articles");

            migrationBuilder.RenameTable(
                name: "Articles",
                newName: "News");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_CommentId",
                table: "News",
                newName: "IX_News_CommentId");

            migrationBuilder.AddColumn<long>(
                name: "NewsRubricRubricId",
                table: "News",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_News",
                table: "News",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_News_NewsRubricRubricId",
                table: "News",
                column: "NewsRubricRubricId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Comments_CommentId",
                table: "News",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Rubrics_NewsRubricRubricId",
                table: "News",
                column: "NewsRubricRubricId",
                principalTable: "Rubrics",
                principalColumn: "RubricId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Comments_CommentId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Rubrics_NewsRubricRubricId",
                table: "News");

            migrationBuilder.DropPrimaryKey(
                name: "PK_News",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_NewsRubricRubricId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "NewsRubricRubricId",
                table: "News");

            migrationBuilder.RenameTable(
                name: "News",
                newName: "Articles");

            migrationBuilder.RenameIndex(
                name: "IX_News_CommentId",
                table: "Articles",
                newName: "IX_Articles_CommentId");

            migrationBuilder.AddColumn<long>(
                name: "ArticleRubricRubricId",
                table: "Articles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles",
                table: "Articles",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleRubricRubricId",
                table: "Articles",
                column: "ArticleRubricRubricId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Rubrics_ArticleRubricRubricId",
                table: "Articles",
                column: "ArticleRubricRubricId",
                principalTable: "Rubrics",
                principalColumn: "RubricId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Comments_CommentId",
                table: "Articles",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
