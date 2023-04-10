using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class addviewModelsDepAndPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptionsWorkerId",
                table: "PostsOfWorker",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DescriptionsWorkerId",
                table: "DepartmentsOfWorker",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostsOfWorker_DescriptionsWorkerId",
                table: "PostsOfWorker",
                column: "DescriptionsWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsOfWorker_DescriptionsWorkerId",
                table: "DepartmentsOfWorker",
                column: "DescriptionsWorkerId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsOfWorker_DescriptionsWorker_DescriptionsWorkerId",
                table: "DepartmentsOfWorker");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsOfWorker_DescriptionsWorker_DescriptionsWorkerId",
                table: "PostsOfWorker");

            migrationBuilder.DropIndex(
                name: "IX_PostsOfWorker_DescriptionsWorkerId",
                table: "PostsOfWorker");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentsOfWorker_DescriptionsWorkerId",
                table: "DepartmentsOfWorker");

            migrationBuilder.DropColumn(
                name: "DescriptionsWorkerId",
                table: "PostsOfWorker");

            migrationBuilder.DropColumn(
                name: "DescriptionsWorkerId",
                table: "DepartmentsOfWorker");
        }
    }
}
