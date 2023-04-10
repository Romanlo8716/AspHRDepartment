using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class AddViewModelDepAndPostWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsOfWorker_DescriptionsWorker_DescriptionsWorkerId",
                table: "DepartmentsOfWorker");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsOfWorker_DescriptionsWorker_DescriptionsWorkerId",
                table: "PostsOfWorker");

            migrationBuilder.RenameColumn(
                name: "DescriptionsWorkerId",
                table: "PostsOfWorker",
                newName: "DepAndPostWorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_PostsOfWorker_DescriptionsWorkerId",
                table: "PostsOfWorker",
                newName: "IX_PostsOfWorker_DepAndPostWorkerId");

            migrationBuilder.RenameColumn(
                name: "DescriptionsWorkerId",
                table: "DepartmentsOfWorker",
                newName: "DepAndPostWorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentsOfWorker_DescriptionsWorkerId",
                table: "DepartmentsOfWorker",
                newName: "IX_DepartmentsOfWorker_DepAndPostWorkerId");

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

            migrationBuilder.CreateIndex(
                name: "IX_DepAndPostWorker_DescriptionsWorkerId",
                table: "DepAndPostWorker",
                column: "DescriptionsWorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsOfWorker_DepAndPostWorker_DepAndPostWorkerId",
                table: "DepartmentsOfWorker",
                column: "DepAndPostWorkerId",
                principalTable: "DepAndPostWorker",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostsOfWorker_DepAndPostWorker_DepAndPostWorkerId",
                table: "PostsOfWorker",
                column: "DepAndPostWorkerId",
                principalTable: "DepAndPostWorker",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsOfWorker_DepAndPostWorker_DepAndPostWorkerId",
                table: "DepartmentsOfWorker");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsOfWorker_DepAndPostWorker_DepAndPostWorkerId",
                table: "PostsOfWorker");

            migrationBuilder.DropTable(
                name: "DepAndPostWorker");

            migrationBuilder.RenameColumn(
                name: "DepAndPostWorkerId",
                table: "PostsOfWorker",
                newName: "DescriptionsWorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_PostsOfWorker_DepAndPostWorkerId",
                table: "PostsOfWorker",
                newName: "IX_PostsOfWorker_DescriptionsWorkerId");

            migrationBuilder.RenameColumn(
                name: "DepAndPostWorkerId",
                table: "DepartmentsOfWorker",
                newName: "DescriptionsWorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentsOfWorker_DepAndPostWorkerId",
                table: "DepartmentsOfWorker",
                newName: "IX_DepartmentsOfWorker_DescriptionsWorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsOfWorker_DescriptionsWorker_DescriptionsWorkerId",
                table: "DepartmentsOfWorker",
                column: "DescriptionsWorkerId",
                principalTable: "DescriptionsWorker",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostsOfWorker_DescriptionsWorker_DescriptionsWorkerId",
                table: "PostsOfWorker",
                column: "DescriptionsWorkerId",
                principalTable: "DescriptionsWorker",
                principalColumn: "Id");
        }
    }
}
