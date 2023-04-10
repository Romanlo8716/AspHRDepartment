using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class deleteDataWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Departments_idDepartment",
                table: "Workers");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Posts_idPost",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_idDepartment",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_idPost",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "idDepartment",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "idPost",
                table: "Workers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idDepartment",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idPost",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_idDepartment",
                table: "Workers",
                column: "idDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_idPost",
                table: "Workers",
                column: "idPost");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Departments_idDepartment",
                table: "Workers",
                column: "idDepartment",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Posts_idPost",
                table: "Workers",
                column: "idPost",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
