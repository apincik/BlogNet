using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddNewProjectSysEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cms_article_settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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

            migrationBuilder.CreateTable(
                name: "user_project_settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                name: "web_menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Url = table.Column<string>(maxLength: 255, nullable: true),
                    ParentMenuId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    MenuId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_web_menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_web_menu_web_menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "web_menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_web_menu_cms_category_ParentMenuId",
                        column: x => x.ParentMenuId,
                        principalTable: "cms_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_web_menu_user_project_ProjectId",
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ShowRaw = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ParentTemplateVariableId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_web_template_variable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_web_template_variable_web_template_variable_ParentTemplateVa~",
                        column: x => x.ParentTemplateVariableId,
                        principalTable: "web_template_variable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_web_template_variable_user_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "user_project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cms_article_settings_ArticleId",
                table: "cms_article_settings",
                column: "ArticleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_project_settings_ProjectId",
                table: "user_project_settings",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_web_menu_MenuId",
                table: "web_menu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_web_menu_ParentMenuId",
                table: "web_menu",
                column: "ParentMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_web_menu_ProjectId",
                table: "web_menu",
                column: "ProjectId",
                unique: true);

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
                name: "web_menu");

            migrationBuilder.DropTable(
                name: "web_page_forward");

            migrationBuilder.DropTable(
                name: "web_template_variable");
        }
    }
}
