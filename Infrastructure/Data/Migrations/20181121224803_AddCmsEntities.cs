using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddCmsEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "seo_onpage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Keywords = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seo_onpage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cms_category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    ParentCategoryId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    SeoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cms_category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cms_category_cms_category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "cms_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cms_category_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cms_category_seo_onpage_SeoId",
                        column: x => x.SeoId,
                        principalTable: "seo_onpage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cms_article",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(maxLength: 255, nullable: false),
                    Tags = table.Column<string>(maxLength: 255, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    SeoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cms_article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cms_article_cms_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "cms_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cms_article_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cms_article_seo_onpage_SeoId",
                        column: x => x.SeoId,
                        principalTable: "seo_onpage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cms_album",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    ArticleId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    NameNormalized = table.Column<string>(maxLength: 255, nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cms_album", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cms_album_cms_article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "cms_article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cms_photo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    AlbumId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    NameNormalized = table.Column<string>(maxLength: 120, nullable: false),
                    Hash = table.Column<string>(maxLength: 64, nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Protocol = table.Column<string>(maxLength: 10, nullable: false),
                    DomainName = table.Column<string>(maxLength: 255, nullable: false),
                    RelativePath = table.Column<string>(maxLength: 255, nullable: false),
                    CdnPath = table.Column<string>(maxLength: 255, nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cms_photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cms_photo_cms_album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "cms_album",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cms_album_ArticleId",
                table: "cms_album",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_CategoryId",
                table: "cms_article",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_ProjectId",
                table: "cms_article",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_SeoId",
                table: "cms_article",
                column: "SeoId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_category_ParentCategoryId",
                table: "cms_category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_category_ProjectId",
                table: "cms_category",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_category_SeoId",
                table: "cms_category",
                column: "SeoId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_photo_AlbumId",
                table: "cms_photo",
                column: "AlbumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cms_photo");

            migrationBuilder.DropTable(
                name: "cms_album");

            migrationBuilder.DropTable(
                name: "cms_article");

            migrationBuilder.DropTable(
                name: "cms_category");

            migrationBuilder.DropTable(
                name: "seo_onpage");
        }
    }
}
