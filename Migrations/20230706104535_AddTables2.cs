using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBookingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_Order_Status_StatusId1",
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

            migrationBuilder.DropColumn(
                name: "StatusId1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "FuelTypeId1",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "TransmissionId1",
                table: "Car");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "TransmissionId",
                table: "Car",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "FuelTypeId",
                table: "Car",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "DriveId",
                table: "Car",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Car",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StatusId",
                table: "Order",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_BrandId",
                table: "Car",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_FuelTypeId",
                table: "Car",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_TransmissionId",
                table: "Car",
                column: "TransmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Brand_BrandId",
                table: "Car",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_FuelType_FuelTypeId",
                table: "Car",
                column: "FuelTypeId",
                principalTable: "FuelType",
                principalColumn: "FuelTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Transmission_TransmissionId",
                table: "Car",
                column: "TransmissionId",
                principalTable: "Transmission",
                principalColumn: "TransmissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Status_StatusId",
                table: "Order",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Brand_BrandId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_FuelType_FuelTypeId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Transmission_TransmissionId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Status_StatusId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_StatusId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Car_BrandId",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_FuelTypeId",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_TransmissionId",
                table: "Car");

            migrationBuilder.AlterColumn<byte>(
                name: "StatusId",
                table: "Order",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StatusId1",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<byte>(
                name: "TransmissionId",
                table: "Car",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte>(
                name: "FuelTypeId",
                table: "Car",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte>(
                name: "DriveId",
                table: "Car",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte>(
                name: "BrandId",
                table: "Car",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BrandId1",
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
                name: "FK_Order_Status_StatusId1",
                table: "Order",
                column: "StatusId1",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
