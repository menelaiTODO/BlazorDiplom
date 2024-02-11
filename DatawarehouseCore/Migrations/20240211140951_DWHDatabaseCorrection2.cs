using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatawarehouseCore.Migrations
{
    /// <inheritdoc />
    public partial class DWHDatabaseCorrection2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "fact_sale_id",
                table: "FactSales",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "dim_product_id",
                table: "DimProducts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "dim_order_id",
                table: "DimOrders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "dim_date_id",
                table: "DimDates",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FactSales",
                table: "FactSales",
                column: "fact_sale_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DimProducts",
                table: "DimProducts",
                column: "dim_product_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DimOrders",
                table: "DimOrders",
                column: "dim_order_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DimDates",
                table: "DimDates",
                column: "dim_date_id");
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
                name: "fact_sale_id",
                table: "FactSales");

            migrationBuilder.DropColumn(
                name: "dim_product_id",
                table: "DimProducts");

            migrationBuilder.DropColumn(
                name: "dim_order_id",
                table: "DimOrders");

            migrationBuilder.DropColumn(
                name: "dim_date_id",
                table: "DimDates");
        }
    }
}
