using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatawarehouseCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FactSales",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DimProducts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DimOrders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DimDates",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FactSales",
                table: "FactSales",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DimProducts",
                table: "DimProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DimOrders",
                table: "DimOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DimDates",
                table: "DimDates",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FactSales",
                table: "FactSales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DimProducts",
                table: "DimProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DimOrders",
                table: "DimOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DimDates",
                table: "DimDates");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FactSales");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DimProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DimOrders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DimDates");
        }
    }
}
