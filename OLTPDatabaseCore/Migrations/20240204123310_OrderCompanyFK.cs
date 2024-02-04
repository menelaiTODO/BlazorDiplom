using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OLTPDatabaseCore.Migrations
{
    /// <inheritdoc />
    public partial class OrderCompanyFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_company_id",
                table: "Orders",
                column: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Companies_company_id",
                table: "Orders",
                column: "company_id",
                principalTable: "Companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Companies_company_id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_company_id",
                table: "Orders");
        }
    }
}
