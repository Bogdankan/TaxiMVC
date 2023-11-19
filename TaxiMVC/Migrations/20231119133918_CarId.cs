using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiMVC.Migrations
{
    public partial class CarId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Car_CarId",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Car_CarId",
                table: "Order",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Car_CarId",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Car_CarId",
                table: "Order",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id");
        }
    }
}
