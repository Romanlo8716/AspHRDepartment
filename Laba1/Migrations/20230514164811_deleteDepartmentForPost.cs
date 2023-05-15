using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class deleteDepartmentForPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Departments_DepartmentId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_DepartmentId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_DepartmentId",
                table: "Posts",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Departments_DepartmentId",
                table: "Posts",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
