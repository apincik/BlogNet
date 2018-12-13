using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateMenuWithCatFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "web_menu");

            migrationBuilder.CreateTable(
                name: "web_menu_item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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

            migrationBuilder.CreateIndex(
                name: "IX_web_menu_item_ParentMenuId",
                table: "web_menu_item",
                column: "ParentMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_web_menu_item_ProjectId",
                table: "web_menu_item",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "web_menu_item");

            migrationBuilder.CreateTable(
                name: "web_menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    MenuId = table.Column<int>(nullable: true),
                    ParentMenuId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Url = table.Column<string>(maxLength: 255, nullable: true)
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
        }
    }
}
