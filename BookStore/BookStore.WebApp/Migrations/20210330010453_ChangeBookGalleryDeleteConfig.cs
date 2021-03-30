using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.WebApp.Migrations
{
    public partial class ChangeBookGalleryDeleteConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksGalleries_Books_BookId",
                table: "BooksGalleries");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksGalleries_Books_BookId",
                table: "BooksGalleries",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksGalleries_Books_BookId",
                table: "BooksGalleries");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksGalleries_Books_BookId",
                table: "BooksGalleries",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
