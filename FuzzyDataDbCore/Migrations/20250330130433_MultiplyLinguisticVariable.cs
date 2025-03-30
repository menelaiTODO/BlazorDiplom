using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuzzyDataDbCore.Migrations
{
    /// <inheritdoc />
    public partial class MultiplyLinguisticVariable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomMultiplyLinguisticVariable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CubeSliceId = table.Column<int>(type: "int", nullable: false),
                    MeasureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomMultiplyLinguisticVariable", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CustomLinguisticVariableCustomMultiplyLinguisticVariable",
                columns: table => new
                {
                    CustomLinguisticVariablesId = table.Column<int>(type: "int", nullable: false),
                    CustomMultiplyLinguisticVariablesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomLinguisticVariableCustomMultiplyLinguisticVariable", x => new { x.CustomLinguisticVariablesId, x.CustomMultiplyLinguisticVariablesId });
                    table.ForeignKey(
                        name: "FK_CustomLinguisticVariableCustomMultiplyLinguisticVariable_CustomLinguisticVariables_CustomLinguisticVariablesId",
                        column: x => x.CustomLinguisticVariablesId,
                        principalTable: "CustomLinguisticVariables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomLinguisticVariableCustomMultiplyLinguisticVariable_CustomMultiplyLinguisticVariable_CustomMultiplyLinguisticVariablesId",
                        column: x => x.CustomMultiplyLinguisticVariablesId,
                        principalTable: "CustomMultiplyLinguisticVariable",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomLinguisticVariableCustomMultiplyLinguisticVariable_CustomMultiplyLinguisticVariablesId",
                table: "CustomLinguisticVariableCustomMultiplyLinguisticVariable",
                column: "CustomMultiplyLinguisticVariablesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomLinguisticVariableCustomMultiplyLinguisticVariable");

            migrationBuilder.DropTable(
                name: "CustomMultiplyLinguisticVariable");
        }
    }
}
