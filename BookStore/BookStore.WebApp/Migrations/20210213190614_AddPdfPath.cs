using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.WebApp.Migrations
{
    public partial class AddPdfPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PdfPath",
                table: "Books",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfPath",
                table: "Books");
        }
    }
}
