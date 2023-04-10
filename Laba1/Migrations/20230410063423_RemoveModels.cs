using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class RemoveModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentsOfWorker");

            migrationBuilder.DropTable(
                name: "PostsOfWorker");

            migrationBuilder.DropTable(
                name: "DepAndPostWorker");

            migrationBuilder.CreateTable(
                name: "DepartmentsAndPostsOfWorker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    DescriptionsWorkerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsAndPostsOfWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentsAndPostsOfWorker_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentsAndPostsOfWorker_DescriptionsWorker_DescriptionsWorkerId",
                        column: x => x.DescriptionsWorkerId,
                        principalTable: "DescriptionsWorker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentsAndPostsOfWorker_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentsAndPostsOfWorker_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsAndPostsOfWorker_DepartmentId",
                table: "DepartmentsAndPostsOfWorker",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsAndPostsOfWorker_DescriptionsWorkerId",
                table: "DepartmentsAndPostsOfWorker",
                column: "DescriptionsWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsAndPostsOfWorker_PostId",
                table: "DepartmentsAndPostsOfWorker",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsAndPostsOfWorker_WorkerId",
                table: "DepartmentsAndPostsOfWorker",
                column: "WorkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentsAndPostsOfWorker");

            migrationBuilder.CreateTable(
                name: "DepAndPostWorker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionsWorkerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepAndPostWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepAndPostWorker_DescriptionsWorker_DescriptionsWorkerId",
                        column: x => x.DescriptionsWorkerId,
                        principalTable: "DescriptionsWorker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepartmentsOfWorker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    DepAndPostWorkerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsOfWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentsOfWorker_DepAndPostWorker_DepAndPostWorkerId",
                        column: x => x.DepAndPostWorkerId,
                        principalTable: "DepAndPostWorker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentsOfWorker_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentsOfWorker_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostsOfWorker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    WorkerId = table.Column<int>(type: "int", nullable: true),
                    DepAndPostWorkerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsOfWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostsOfWorker_DepAndPostWorker_DepAndPostWorkerId",
                        column: x => x.DepAndPostWorkerId,
                        principalTable: "DepAndPostWorker",
                        principalColumn: "Id");
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
                name: "IX_DepAndPostWorker_DescriptionsWorkerId",
                table: "DepAndPostWorker",
                column: "DescriptionsWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsOfWorker_DepAndPostWorkerId",
                table: "DepartmentsOfWorker",
                column: "DepAndPostWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsOfWorker_DepartmentId",
                table: "DepartmentsOfWorker",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsOfWorker_WorkerId",
                table: "DepartmentsOfWorker",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsOfWorker_DepAndPostWorkerId",
                table: "PostsOfWorker",
                column: "DepAndPostWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsOfWorker_PostId",
                table: "PostsOfWorker",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsOfWorker_WorkerId",
                table: "PostsOfWorker",
                column: "WorkerId");
        }
    }
}
