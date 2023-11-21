using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLib.Migrations
{
    public partial class readingstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasFinished",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsReading",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "ReadingStatus",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadingStatus",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "HasFinished",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReading",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
