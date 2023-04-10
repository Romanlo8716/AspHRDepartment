using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class probNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsOfWorker_Departments_DepartmentId",
                table: "DepartmentsOfWorker");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsOfWorker_Workers_WorkerId",
                table: "DepartmentsOfWorker");

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "DepartmentsOfWorker",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "DepartmentsOfWorker",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsOfWorker_Departments_DepartmentId",
                table: "DepartmentsOfWorker",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsOfWorker_Workers_WorkerId",
                table: "DepartmentsOfWorker",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsOfWorker_Departments_DepartmentId",
                table: "DepartmentsOfWorker");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsOfWorker_Workers_WorkerId",
                table: "DepartmentsOfWorker");

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "DepartmentsOfWorker",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "DepartmentsOfWorker",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsOfWorker_Departments_DepartmentId",
                table: "DepartmentsOfWorker",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsOfWorker_Workers_WorkerId",
                table: "DepartmentsOfWorker",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }
    }
}
