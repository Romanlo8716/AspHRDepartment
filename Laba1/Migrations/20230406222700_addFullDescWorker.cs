using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class addFullDescWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptionsWorkerId",
                table: "Vacations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DescriptionsWorkerId",
                table: "MedicalBook",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DescriptionsWorkerId",
                table: "Educations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_DescriptionsWorkerId",
                table: "Vacations",
                column: "DescriptionsWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalBook_DescriptionsWorkerId",
                table: "MedicalBook",
                column: "DescriptionsWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_DescriptionsWorkerId",
                table: "Educations",
                column: "DescriptionsWorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_DescriptionsWorker_DescriptionsWorkerId",
                table: "Educations",
                column: "DescriptionsWorkerId",
                principalTable: "DescriptionsWorker",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalBook_DescriptionsWorker_DescriptionsWorkerId",
                table: "MedicalBook",
                column: "DescriptionsWorkerId",
                principalTable: "DescriptionsWorker",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_DescriptionsWorker_DescriptionsWorkerId",
                table: "Vacations",
                column: "DescriptionsWorkerId",
                principalTable: "DescriptionsWorker",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_DescriptionsWorker_DescriptionsWorkerId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalBook_DescriptionsWorker_DescriptionsWorkerId",
                table: "MedicalBook");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_DescriptionsWorker_DescriptionsWorkerId",
                table: "Vacations");

            migrationBuilder.DropIndex(
                name: "IX_Vacations_DescriptionsWorkerId",
                table: "Vacations");

            migrationBuilder.DropIndex(
                name: "IX_MedicalBook_DescriptionsWorkerId",
                table: "MedicalBook");

            migrationBuilder.DropIndex(
                name: "IX_Educations_DescriptionsWorkerId",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "DescriptionsWorkerId",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "DescriptionsWorkerId",
                table: "MedicalBook");

            migrationBuilder.DropColumn(
                name: "DescriptionsWorkerId",
                table: "Educations");
        }
    }
}
