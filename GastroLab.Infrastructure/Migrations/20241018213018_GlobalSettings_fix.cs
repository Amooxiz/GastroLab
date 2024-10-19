using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastroLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GlobalSettings_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaultWaitingTime",
                table: "GlobalSettings",
                newName: "DefaultDineInWaitingTime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DefaultDeliveryWaitingTime",
                table: "GlobalSettings",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultDeliveryWaitingTime",
                table: "GlobalSettings");

            migrationBuilder.RenameColumn(
                name: "DefaultDineInWaitingTime",
                table: "GlobalSettings",
                newName: "DefaultWaitingTime");
        }
    }
}
