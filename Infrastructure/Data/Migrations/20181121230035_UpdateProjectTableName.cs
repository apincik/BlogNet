using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateProjectTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cms_article_Projects_ProjectId",
                table: "cms_article");

            migrationBuilder.DropForeignKey(
                name: "FK_cms_category_Projects_ProjectId",
                table: "cms_category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "user_project");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_project",
                table: "user_project",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cms_article_user_project_ProjectId",
                table: "cms_article",
                column: "ProjectId",
                principalTable: "user_project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cms_category_user_project_ProjectId",
                table: "cms_category",
                column: "ProjectId",
                principalTable: "user_project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cms_article_user_project_ProjectId",
                table: "cms_article");

            migrationBuilder.DropForeignKey(
                name: "FK_cms_category_user_project_ProjectId",
                table: "cms_category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_project",
                table: "user_project");

            migrationBuilder.RenameTable(
                name: "user_project",
                newName: "Projects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cms_article_Projects_ProjectId",
                table: "cms_article",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cms_category_Projects_ProjectId",
                table: "cms_category",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
