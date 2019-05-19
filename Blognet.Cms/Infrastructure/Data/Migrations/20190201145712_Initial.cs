using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Blognet.Cms.Infrastructure.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cms_album",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    NameNormalized = table.Column<string>(maxLength: 255, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cms_album", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "seo_onpage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "user_project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    DomainName = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cms_photo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    AlbumId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    NameNormalized = table.Column<string>(maxLength: 120, nullable: false),
                    Extension = table.Column<string>(maxLength: 10, nullable: false),
                    Hash = table.Column<string>(maxLength: 64, nullable: false),
                    ImageHash = table.Column<string>(maxLength: 64, nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Protocol = table.Column<string>(maxLength: 10, nullable: true),
                    DomainName = table.Column<string>(maxLength: 255, nullable: true),
                    RelativePath = table.Column<string>(maxLength: 255, nullable: false),
                    CdnPath = table.Column<string>(maxLength: 255, nullable: true),
                    OriginSource = table.Column<string>(maxLength: 255, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    IsLocal = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "cms_category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        name: "FK_cms_category_user_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "user_project",
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
                name: "user_project_settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    ArticleThumbnailWidth = table.Column<int>(nullable: true),
                    ArticleThumbnailHeight = table.Column<int>(nullable: true),
                    ArticleHeaderWidth = table.Column<int>(nullable: true),
                    ArticleHeaderHeight = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_project_settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_project_settings_user_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "user_project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "web_menu_item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Url = table.Column<string>(maxLength: 255, nullable: true),
                    ParentMenuId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_web_menu_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_web_menu_item_web_menu_item_ParentMenuId",
                        column: x => x.ParentMenuId,
                        principalTable: "web_menu_item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_web_menu_item_user_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "user_project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "web_page_forward",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Mask = table.Column<string>(maxLength: 255, nullable: true),
                    DestinationUrl = table.Column<string>(maxLength: 255, nullable: true),
                    SourceId = table.Column<string>(maxLength: 255, nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_web_page_forward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_web_page_forward_user_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "user_project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "web_template_variable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ShowRaw = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ParentTemplateVariableId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_web_template_variable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_web_template_variable_web_template_variable_ParentTemplateV~",
                        column: x => x.ParentTemplateVariableId,
                        principalTable: "web_template_variable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_web_template_variable_user_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "user_project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cms_article",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(maxLength: 255, nullable: false),
                    Tags = table.Column<string>(maxLength: 255, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    SeoId = table.Column<int>(nullable: true),
                    PhotoThumbnailId = table.Column<int>(nullable: true),
                    PhotoHeaderId = table.Column<int>(nullable: true),
                    AlbumId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cms_article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cms_article_cms_album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "cms_album",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cms_article_cms_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "cms_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cms_article_cms_photo_PhotoHeaderId",
                        column: x => x.PhotoHeaderId,
                        principalTable: "cms_photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cms_article_cms_photo_PhotoThumbnailId",
                        column: x => x.PhotoThumbnailId,
                        principalTable: "cms_photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cms_article_user_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "user_project",
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
                name: "cms_article_settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    PageAdsDensity = table.Column<int>(nullable: false),
                    ShowSocialPlugins = table.Column<bool>(nullable: false),
                    ShowComments = table.Column<bool>(nullable: false),
                    UpdateSlugOnTitleChange = table.Column<bool>(nullable: false),
                    AdsActive = table.Column<bool>(nullable: false),
                    Nsfw = table.Column<bool>(nullable: false),
                    ArticleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cms_article_settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cms_article_settings_cms_article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "cms_article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_AlbumId",
                table: "cms_article",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_CategoryId",
                table: "cms_article",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_PhotoHeaderId",
                table: "cms_article",
                column: "PhotoHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_PhotoThumbnailId",
                table: "cms_article",
                column: "PhotoThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_ProjectId",
                table: "cms_article",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_SeoId",
                table: "cms_article",
                column: "SeoId");

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_Slug_ProjectId",
                table: "cms_article",
                columns: new[] { "Slug", "ProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_settings_ArticleId",
                table: "cms_article_settings",
                column: "ArticleId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_user_project_settings_ProjectId",
                table: "user_project_settings",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_web_menu_item_ParentMenuId",
                table: "web_menu_item",
                column: "ParentMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_web_menu_item_ProjectId",
                table: "web_menu_item",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_web_page_forward_ProjectId",
                table: "web_page_forward",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_web_template_variable_ParentTemplateVariableId",
                table: "web_template_variable",
                column: "ParentTemplateVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_web_template_variable_ProjectId",
                table: "web_template_variable",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cms_article_settings");

            migrationBuilder.DropTable(
                name: "user_project_settings");

            migrationBuilder.DropTable(
                name: "web_menu_item");

            migrationBuilder.DropTable(
                name: "web_page_forward");

            migrationBuilder.DropTable(
                name: "web_template_variable");

            migrationBuilder.DropTable(
                name: "cms_article");

            migrationBuilder.DropTable(
                name: "cms_category");

            migrationBuilder.DropTable(
                name: "cms_photo");

            migrationBuilder.DropTable(
                name: "user_project");

            migrationBuilder.DropTable(
                name: "seo_onpage");

            migrationBuilder.DropTable(
                name: "cms_album");
        }
    }
}
