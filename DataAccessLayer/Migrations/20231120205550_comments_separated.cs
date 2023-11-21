using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class comments_separated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComment_Blogs_BlogId",
                table: "BlogComment");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogComment_Customers_CustomerId",
                table: "BlogComment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Customers_CustomerId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_TripDate_TripDateId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Trips_TripId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTrips_TripDate_TripDateId",
                table: "CustomerTrips");

            migrationBuilder.DropForeignKey(
                name: "FK_TripDate_Trips_TripId",
                table: "TripDate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripDate",
                table: "TripDate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogComment",
                table: "BlogComment");

            migrationBuilder.RenameTable(
                name: "TripDate",
                newName: "TripDates");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "TripComments");

            migrationBuilder.RenameTable(
                name: "BlogComment",
                newName: "BlogComments");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Trips",
                newName: "Content");

            migrationBuilder.RenameIndex(
                name: "IX_TripDate_TripId",
                table: "TripDates",
                newName: "IX_TripDates_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TripId",
                table: "TripComments",
                newName: "IX_TripComments_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TripDateId",
                table: "TripComments",
                newName: "IX_TripComments_TripDateId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CustomerId",
                table: "TripComments",
                newName: "IX_TripComments_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComment_CustomerId",
                table: "BlogComments",
                newName: "IX_BlogComments_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComment_BlogId",
                table: "BlogComments",
                newName: "IX_BlogComments_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripDates",
                table: "TripDates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripComments",
                table: "TripComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogComments",
                table: "BlogComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_Blogs_BlogId",
                table: "BlogComments",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_Customers_CustomerId",
                table: "BlogComments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTrips_TripDates_TripDateId",
                table: "CustomerTrips",
                column: "TripDateId",
                principalTable: "TripDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripComments_Customers_CustomerId",
                table: "TripComments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripComments_TripDates_TripDateId",
                table: "TripComments",
                column: "TripDateId",
                principalTable: "TripDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripComments_Trips_TripId",
                table: "TripComments",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripDates_Trips_TripId",
                table: "TripDates",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_Blogs_BlogId",
                table: "BlogComments");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_Customers_CustomerId",
                table: "BlogComments");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTrips_TripDates_TripDateId",
                table: "CustomerTrips");

            migrationBuilder.DropForeignKey(
                name: "FK_TripComments_Customers_CustomerId",
                table: "TripComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TripComments_TripDates_TripDateId",
                table: "TripComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TripComments_Trips_TripId",
                table: "TripComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TripDates_Trips_TripId",
                table: "TripDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripDates",
                table: "TripDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripComments",
                table: "TripComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogComments",
                table: "BlogComments");

            migrationBuilder.RenameTable(
                name: "TripDates",
                newName: "TripDate");

            migrationBuilder.RenameTable(
                name: "TripComments",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "BlogComments",
                newName: "BlogComment");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Trips",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_TripDates_TripId",
                table: "TripDate",
                newName: "IX_TripDate_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_TripComments_TripId",
                table: "Comments",
                newName: "IX_Comments_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_TripComments_TripDateId",
                table: "Comments",
                newName: "IX_Comments_TripDateId");

            migrationBuilder.RenameIndex(
                name: "IX_TripComments_CustomerId",
                table: "Comments",
                newName: "IX_Comments_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComments_CustomerId",
                table: "BlogComment",
                newName: "IX_BlogComment_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComments_BlogId",
                table: "BlogComment",
                newName: "IX_BlogComment_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripDate",
                table: "TripDate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogComment",
                table: "BlogComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComment_Blogs_BlogId",
                table: "BlogComment",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComment_Customers_CustomerId",
                table: "BlogComment",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Customers_CustomerId",
                table: "Comments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_TripDate_TripDateId",
                table: "Comments",
                column: "TripDateId",
                principalTable: "TripDate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Trips_TripId",
                table: "Comments",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTrips_TripDate_TripDateId",
                table: "CustomerTrips",
                column: "TripDateId",
                principalTable: "TripDate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripDate_Trips_TripId",
                table: "TripDate",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
