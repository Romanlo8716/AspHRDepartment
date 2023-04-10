using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laba1.Migrations
{
    /// <inheritdoc />
    public partial class addLaborBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LaborBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateRecord = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nameWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    intelligenceWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nameDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numberDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaborBook_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LaborBook_WorkerId",
                table: "LaborBook",
                column: "WorkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaborBook");
        }
    }
}
