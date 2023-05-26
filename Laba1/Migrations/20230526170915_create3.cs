using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class create3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostsOfDepartment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsOfDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostsOfDepartment_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsOfDepartment_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostsOfDepartment_DepartmentId",
                table: "PostsOfDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsOfDepartment_PostId",
                table: "PostsOfDepartment",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostsOfDepartment");
        }
    }
}
