using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuzzyDataDbCore.Migrations
{
    /// <inheritdoc />
    public partial class MultiplyLinguisticVariable2_0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomLinguisticVariableCustomMultiplyLinguisticVariable_CustomMultiplyLinguisticVariable_CustomMultiplyLinguisticVariablesId",
                table: "CustomLinguisticVariableCustomMultiplyLinguisticVariable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomMultiplyLinguisticVariable",
                table: "CustomMultiplyLinguisticVariable");

            migrationBuilder.RenameTable(
                name: "CustomMultiplyLinguisticVariable",
                newName: "CustomMultiplyLinguisticVariables");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomMultiplyLinguisticVariables",
                table: "CustomMultiplyLinguisticVariables",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomLinguisticVariableCustomMultiplyLinguisticVariable_CustomMultiplyLinguisticVariables_CustomMultiplyLinguisticVariables~",
                table: "CustomLinguisticVariableCustomMultiplyLinguisticVariable",
                column: "CustomMultiplyLinguisticVariablesId",
                principalTable: "CustomMultiplyLinguisticVariables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomLinguisticVariableCustomMultiplyLinguisticVariable_CustomMultiplyLinguisticVariables_CustomMultiplyLinguisticVariables~",
                table: "CustomLinguisticVariableCustomMultiplyLinguisticVariable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomMultiplyLinguisticVariables",
                table: "CustomMultiplyLinguisticVariables");

            migrationBuilder.RenameTable(
                name: "CustomMultiplyLinguisticVariables",
                newName: "CustomMultiplyLinguisticVariable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomMultiplyLinguisticVariable",
                table: "CustomMultiplyLinguisticVariable",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomLinguisticVariableCustomMultiplyLinguisticVariable_CustomMultiplyLinguisticVariable_CustomMultiplyLinguisticVariablesId",
                table: "CustomLinguisticVariableCustomMultiplyLinguisticVariable",
                column: "CustomMultiplyLinguisticVariablesId",
                principalTable: "CustomMultiplyLinguisticVariable",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
