using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastroLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Option_new_attributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSelected",
                table: "Options");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Options",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Options",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Options");

            migrationBuilder.AddColumn<bool>(
                name: "isSelected",
                table: "Options",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
