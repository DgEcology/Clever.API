using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clever.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationsBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ReactionId",
                table: "Events");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AttendanceId",
                table: "Events",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ReactionId",
                table: "Events",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
