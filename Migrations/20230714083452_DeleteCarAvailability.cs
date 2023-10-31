using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBookingApp.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCarAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Car");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Availability",
                table: "Car",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
