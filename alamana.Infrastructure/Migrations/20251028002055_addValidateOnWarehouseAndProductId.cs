using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alamana.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addValidateOnWarehouseAndProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseProducts_warehouseId",
                table: "WarehouseProducts");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProducts_warehouseId_productId",
                table: "WarehouseProducts",
                columns: new[] { "warehouseId", "productId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseProducts_warehouseId_productId",
                table: "WarehouseProducts");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProducts_warehouseId",
                table: "WarehouseProducts",
                column: "warehouseId");
        }
    }
}
