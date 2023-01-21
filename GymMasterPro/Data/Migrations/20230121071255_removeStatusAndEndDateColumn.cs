using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymMasterPro.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeStatusAndEndDateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Checkins");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Checkins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Checkins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Checkins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
