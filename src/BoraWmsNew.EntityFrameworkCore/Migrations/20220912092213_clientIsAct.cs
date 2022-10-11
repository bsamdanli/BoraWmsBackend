using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoraWmsNew.Migrations
{
    public partial class clientIsAct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Client",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Client");
        }
    }
}
