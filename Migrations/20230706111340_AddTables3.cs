using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBookingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTables3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriveType",
                table: "Car");

            migrationBuilder.CreateIndex(
                name: "IX_Car_DriveId",
                table: "Car",
                column: "DriveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Drive_DriveId",
                table: "Car",
                column: "DriveId",
                principalTable: "Drive",
                principalColumn: "DriveId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Drive_DriveId",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_DriveId",
                table: "Car");

            migrationBuilder.AddColumn<int>(
                name: "DriveType",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
