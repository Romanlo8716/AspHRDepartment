using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class addFullDescOfWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CityHabitation",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DateOfBirth",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DateOfIssue",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionWorker",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DivisionCode",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyStatus",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HouseHabitation",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IssuedByWhom",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberInn",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberPass",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberSnils",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeriesPass",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StreetHabitation",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Children",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "CityHabitation",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "DescriptionWorker",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "DivisionCode",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "FamilyStatus",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "HouseHabitation",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "IssuedByWhom",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "NumberInn",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "NumberPass",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "NumberSnils",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "SeriesPass",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "StreetHabitation",
                table: "Workers");
        }
    }
}
