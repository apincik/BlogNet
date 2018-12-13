using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class UpdatePhotoPathAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Protocol",
                table: "cms_photo",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DomainName",
                table: "cms_photo",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocal",
                table: "cms_photo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "cms_photo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocal",
                table: "cms_photo");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "cms_photo");

            migrationBuilder.AlterColumn<string>(
                name: "Protocol",
                table: "cms_photo",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DomainName",
                table: "cms_photo",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
