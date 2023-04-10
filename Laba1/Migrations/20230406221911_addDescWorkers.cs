using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class addDescWorkers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptionsWorkerId",
                table: "LaborBook",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DescriptionsWorker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    workerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionsWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DescriptionsWorker_Workers_workerId",
                        column: x => x.workerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LaborBook_DescriptionsWorkerId",
                table: "LaborBook",
                column: "DescriptionsWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionsWorker_workerId",
                table: "DescriptionsWorker",
                column: "workerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LaborBook_DescriptionsWorker_DescriptionsWorkerId",
                table: "LaborBook",
                column: "DescriptionsWorkerId",
                principalTable: "DescriptionsWorker",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaborBook_DescriptionsWorker_DescriptionsWorkerId",
                table: "LaborBook");

            migrationBuilder.DropTable(
                name: "DescriptionsWorker");

            migrationBuilder.DropIndex(
                name: "IX_LaborBook_DescriptionsWorkerId",
                table: "LaborBook");

            migrationBuilder.DropColumn(
                name: "DescriptionsWorkerId",
                table: "LaborBook");
        }
    }
}
