using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class WebMarketContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_OrderDetails_ProductId_OrderId",
                table: "OrderDetails",
                columns: new[] { "ProductId", "OrderId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_OrderDetails_ProductId_OrderId",
                table: "OrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");
        }
    }
}
