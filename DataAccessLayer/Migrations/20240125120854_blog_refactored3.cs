using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class blog_refactored3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Customers_CustomerId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_CustomerId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Blogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Blogs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CustomerId",
                table: "Blogs",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Customers_CustomerId",
                table: "Blogs",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
