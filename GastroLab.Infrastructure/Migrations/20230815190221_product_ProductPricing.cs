using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastroLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class product_ProductPricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productPricings_Products_ProductId",
                table: "productPricings");

            migrationBuilder.DropIndex(
                name: "IX_productPricings_ProductId",
                table: "productPricings");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "productPricings");

            migrationBuilder.AddColumn<int>(
                name: "ProductPricingId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductPricingId",
                table: "Products",
                column: "ProductPricingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_productPricings_ProductPricingId",
                table: "Products",
                column: "ProductPricingId",
                principalTable: "productPricings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_productPricings_ProductPricingId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductPricingId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductPricingId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "productPricings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_productPricings_ProductId",
                table: "productPricings",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_productPricings_Products_ProductId",
                table: "productPricings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
