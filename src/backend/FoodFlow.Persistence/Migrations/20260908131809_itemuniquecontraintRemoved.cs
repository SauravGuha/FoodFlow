using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class itemuniquecontraintRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Item_RestaurantId_CuisineId",
                table: "Item");

            migrationBuilder.CreateIndex(
                name: "IX_Item_RestaurantId_CuisineId",
                table: "Item",
                columns: new[] { "RestaurantId", "CuisineId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Item_RestaurantId_CuisineId",
                table: "Item");

            migrationBuilder.CreateIndex(
                name: "IX_Item_RestaurantId_CuisineId",
                table: "Item",
                columns: new[] { "RestaurantId", "CuisineId" },
                unique: true);
        }
    }
}
