using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alamana.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatewarehousecategoryvalidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseCategories_warehouseId",
                table: "WarehouseCategories");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseCategories_warehouseId_categoryId",
                table: "WarehouseCategories",
                columns: new[] { "warehouseId", "categoryId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseCategories_warehouseId_categoryId",
                table: "WarehouseCategories");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseCategories_warehouseId",
                table: "WarehouseCategories",
                column: "warehouseId");
        }
    }
}
