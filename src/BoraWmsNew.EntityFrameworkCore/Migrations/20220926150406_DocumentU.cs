using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoraWmsNew.Migrations
{
    public partial class DocumentU : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Documents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Documents");
        }
    }
}
