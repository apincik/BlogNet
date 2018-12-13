using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateArticleImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cms_article_cms_photo_PhotoHeaderId",
                table: "cms_article");

            migrationBuilder.DropForeignKey(
                name: "FK_cms_article_cms_photo_PhotoThumbnailId",
                table: "cms_article");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoThumbnailId",
                table: "cms_article",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PhotoHeaderId",
                table: "cms_article",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_cms_article_cms_photo_PhotoHeaderId",
                table: "cms_article",
                column: "PhotoHeaderId",
                principalTable: "cms_photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cms_article_cms_photo_PhotoThumbnailId",
                table: "cms_article",
                column: "PhotoThumbnailId",
                principalTable: "cms_photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cms_article_cms_photo_PhotoHeaderId",
                table: "cms_article");

            migrationBuilder.DropForeignKey(
                name: "FK_cms_article_cms_photo_PhotoThumbnailId",
                table: "cms_article");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoThumbnailId",
                table: "cms_article",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PhotoHeaderId",
                table: "cms_article",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cms_article_cms_photo_PhotoHeaderId",
                table: "cms_article",
                column: "PhotoHeaderId",
                principalTable: "cms_photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cms_article_cms_photo_PhotoThumbnailId",
                table: "cms_article",
                column: "PhotoThumbnailId",
                principalTable: "cms_photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
