using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateArticleDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_web_template_variable_user_project_ProjectId",
                table: "web_template_variable");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "web_template_variable",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DomainName",
                table: "user_project",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "cms_article",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_web_template_variable_user_project_ProjectId",
                table: "web_template_variable",
                column: "ProjectId",
                principalTable: "user_project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_web_template_variable_user_project_ProjectId",
                table: "web_template_variable");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "web_template_variable",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "DomainName",
                table: "user_project",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "cms_article",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_web_template_variable_user_project_ProjectId",
                table: "web_template_variable",
                column: "ProjectId",
                principalTable: "user_project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
