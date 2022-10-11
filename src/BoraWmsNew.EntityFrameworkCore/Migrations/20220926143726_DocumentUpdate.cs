using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoraWmsNew.Migrations
{
    public partial class DocumentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Products_ProductId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ProductId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Documents");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "DocumentDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "DocumentDetails");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ProductId",
                table: "Documents",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Products_ProductId",
                table: "Documents",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
