using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.WebApp.Migrations
{
    public partial class AddCoverPathColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverPhotoPath",
                table: "Books",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPhotoPath",
                table: "Books");
        }
    }
}
