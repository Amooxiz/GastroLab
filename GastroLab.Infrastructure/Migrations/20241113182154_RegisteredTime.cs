using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastroLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RegisteredTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RegisteredTimes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RegisteredTimes");
        }
    }
}
