using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateArticleAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cms_album_cms_article_ArticleId",
                table: "cms_album");

            migrationBuilder.DropIndex(
                name: "IX_cms_album_ArticleId",
                table: "cms_album");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "cms_album");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "cms_article",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhotoHeaderId",
                table: "cms_article",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhotoThumbnailId",
                table: "cms_article",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "cms_album",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_AlbumId",
                table: "cms_article",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_PhotoHeaderId",
                table: "cms_article",
                column: "PhotoHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_PhotoThumbnailId",
                table: "cms_article",
                column: "PhotoThumbnailId");

            migrationBuilder.AddForeignKey(
                name: "FK_cms_article_cms_album_AlbumId",
                table: "cms_article",
                column: "AlbumId",
                principalTable: "cms_album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cms_article_cms_album_AlbumId",
                table: "cms_article");

            migrationBuilder.DropForeignKey(
                name: "FK_cms_article_cms_photo_PhotoHeaderId",
                table: "cms_article");

            migrationBuilder.DropForeignKey(
                name: "FK_cms_article_cms_photo_PhotoThumbnailId",
                table: "cms_article");

            migrationBuilder.DropIndex(
                name: "IX_cms_article_AlbumId",
                table: "cms_article");

            migrationBuilder.DropIndex(
                name: "IX_cms_article_PhotoHeaderId",
                table: "cms_article");

            migrationBuilder.DropIndex(
                name: "IX_cms_article_PhotoThumbnailId",
                table: "cms_article");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "cms_article");

            migrationBuilder.DropColumn(
                name: "PhotoHeaderId",
                table: "cms_article");

            migrationBuilder.DropColumn(
                name: "PhotoThumbnailId",
                table: "cms_article");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "cms_album");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "cms_album",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cms_album_ArticleId",
                table: "cms_album",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_cms_album_cms_article_ArticleId",
                table: "cms_album",
                column: "ArticleId",
                principalTable: "cms_article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
