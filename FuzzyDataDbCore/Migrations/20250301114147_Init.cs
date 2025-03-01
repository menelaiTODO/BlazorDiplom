using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuzzyDataDbCore.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomLinguisticVariables",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinIndex = table.Column<double>(type: "float", nullable: false),
                    CubeSliceId = table.Column<int>(type: "int", nullable: false),
                    FuncId = table.Column<int>(type: "int", nullable: false),
                    MeasureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomLinguisticVariables", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomLinguisticVariableId = table.Column<int>(type: "int", nullable: false),
                    x_value = table.Column<double>(type: "float", nullable: false),
                    y_value = table.Column<double>(type: "float", nullable: false),
                    PointSeq = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.id);
                    table.ForeignKey(
                        name: "FK_Point_CustomLinguisticVariables_CustomLinguisticVariableId",
                        column: x => x.CustomLinguisticVariableId,
                        principalTable: "CustomLinguisticVariables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Point_CustomLinguisticVariableId",
                table: "Point",
                column: "CustomLinguisticVariableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DropTable(
                name: "CustomLinguisticVariables");
        }
    }
}
