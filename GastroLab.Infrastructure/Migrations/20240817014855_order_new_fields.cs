using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastroLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class order_new_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledDeliveryDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "WaitingTime",
                table: "Orders",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isScheduledDelivery",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledDeliveryDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WaitingTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "isScheduledDelivery",
                table: "Orders");
        }
    }
}
