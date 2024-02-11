using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatawarehouseCore.Migrations
{
    /// <inheritdoc />
    public partial class DWHDatabaseCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Sum",
                table: "FactSales",
                newName: "sum");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "FactSales",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "FactSales",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "FactSales",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "DimDates",
                newName: "year");

            migrationBuilder.RenameColumn(
                name: "Month",
                table: "DimDates",
                newName: "month");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "DimDates",
                newName: "day");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "DimDates",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "MonthName",
                table: "DimDates",
                newName: "month_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sum",
                table: "FactSales",
                newName: "Sum");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "FactSales",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "FactSales",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "FactSales",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "year",
                table: "DimDates",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "month",
                table: "DimDates",
                newName: "Month");

            migrationBuilder.RenameColumn(
                name: "day",
                table: "DimDates",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "DimDates",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "month_name",
                table: "DimDates",
                newName: "MonthName");

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
    }
}
