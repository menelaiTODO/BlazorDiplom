using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuzzyDataDbCore.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Point_CustomLinguisticVariables_CustomLinguisticVariableId",
                table: "Point");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Point",
                table: "Point");

            migrationBuilder.RenameTable(
                name: "Point",
                newName: "Points");

            migrationBuilder.RenameIndex(
                name: "IX_Point_CustomLinguisticVariableId",
                table: "Points",
                newName: "IX_Points_CustomLinguisticVariableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Points",
                table: "Points",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Points_CustomLinguisticVariables_CustomLinguisticVariableId",
                table: "Points",
                column: "CustomLinguisticVariableId",
                principalTable: "CustomLinguisticVariables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Points_CustomLinguisticVariables_CustomLinguisticVariableId",
                table: "Points");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Points",
                table: "Points");

            migrationBuilder.RenameTable(
                name: "Points",
                newName: "Point");

            migrationBuilder.RenameIndex(
                name: "IX_Points_CustomLinguisticVariableId",
                table: "Point",
                newName: "IX_Point_CustomLinguisticVariableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Point",
                table: "Point",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Point_CustomLinguisticVariables_CustomLinguisticVariableId",
                table: "Point",
                column: "CustomLinguisticVariableId",
                principalTable: "CustomLinguisticVariables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
