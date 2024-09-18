using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clever.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendanceStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassportData",
                table: "OrganiserApplications",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "AboutMe",
                table: "OrganiserApplications",
                newName: "OrganisationNumber");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "OrganiserApplications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrganiserApplications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrganisationName",
                table: "OrganiserApplications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Attendance",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "OrganiserApplications");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrganiserApplications");

            migrationBuilder.DropColumn(
                name: "OrganisationName",
                table: "OrganiserApplications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Attendance");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "OrganiserApplications",
                newName: "PassportData");

            migrationBuilder.RenameColumn(
                name: "OrganisationNumber",
                table: "OrganiserApplications",
                newName: "AboutMe");
        }
    }
}
