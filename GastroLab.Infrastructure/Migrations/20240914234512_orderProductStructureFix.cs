using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastroLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class orderProductStructureFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductOptions_Orders_OrderId",
                table: "OrderProductOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductOptions_Products_ProductId",
                table: "OrderProductOptions");

            migrationBuilder.DropIndex(
                name: "IX_OrderProductOptions_OrderId",
                table: "OrderProductOptions");

            migrationBuilder.DropIndex(
                name: "IX_OrderProductOptions_ProductId",
                table: "OrderProductOptions");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderProductOptions");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderProductOptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderProductOptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderProductOptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductOptions_OrderId",
                table: "OrderProductOptions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductOptions_ProductId",
                table: "OrderProductOptions",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductOptions_Orders_OrderId",
                table: "OrderProductOptions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductOptions_Products_ProductId",
                table: "OrderProductOptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
