using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class addPostsAndDepartmentsOfWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentsOfWorker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsOfWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentsOfWorker_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentsOfWorker_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostsOfWorker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsOfWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostsOfWorker_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostsOfWorker_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsOfWorker_DepartmentId",
                table: "DepartmentsOfWorker",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsOfWorker_WorkerId",
                table: "DepartmentsOfWorker",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsOfWorker_PostId",
                table: "PostsOfWorker",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsOfWorker_WorkerId",
                table: "PostsOfWorker",
                column: "WorkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentsOfWorker");

            migrationBuilder.DropTable(
                name: "PostsOfWorker");
        }
    }
}
