using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastroLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ingredient_price_deleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Ingredients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Ingredients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
