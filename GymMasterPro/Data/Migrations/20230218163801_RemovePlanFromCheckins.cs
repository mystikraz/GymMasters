using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymMasterPro.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePlanFromCheckins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkins_Plans_PlanId",
                table: "Checkins");

            migrationBuilder.DropIndex(
                name: "IX_Checkins_PlanId",
                table: "Checkins");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Checkins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Checkins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_PlanId",
                table: "Checkins",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkins_Plans_PlanId",
                table: "Checkins",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
