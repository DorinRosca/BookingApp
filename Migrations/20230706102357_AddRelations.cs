using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBookingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId1",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrandId1",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DriveType",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuelTypeId1",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransmissionId1",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CarId",
                table: "Order",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StatusId1",
                table: "Order",
                column: "StatusId1");

            migrationBuilder.CreateIndex(
                name: "IX_Car_BrandId1",
                table: "Car",
                column: "BrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_Car_FuelTypeId1",
                table: "Car",
                column: "FuelTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Car_TransmissionId1",
                table: "Car",
                column: "TransmissionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Car_VehicleId",
                table: "Car",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Brand_BrandId1",
                table: "Car",
                column: "BrandId1",
                principalTable: "Brand",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_FuelType_FuelTypeId1",
                table: "Car",
                column: "FuelTypeId1",
                principalTable: "FuelType",
                principalColumn: "FuelTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Transmission_TransmissionId1",
                table: "Car",
                column: "TransmissionId1",
                principalTable: "Transmission",
                principalColumn: "TransmissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Vehicle_VehicleId",
                table: "Car",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Car_CarId",
                table: "Order",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Status_StatusId1",
                table: "Order",
                column: "StatusId1",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Brand_BrandId1",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_FuelType_FuelTypeId1",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Transmission_TransmissionId1",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Vehicle_VehicleId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Car_CarId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Status_StatusId1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CarId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_StatusId1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Car_BrandId1",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_FuelTypeId1",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_TransmissionId1",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_VehicleId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "StatusId1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "DriveType",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "FuelTypeId1",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "TransmissionId1",
                table: "Car");
        }
    }
}
