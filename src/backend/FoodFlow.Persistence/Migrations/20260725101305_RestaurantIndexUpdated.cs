using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantIndexUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_FNumber",
                table: "Restaurants",
                column: "FNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_GstNumber",
                table: "Restaurants",
                column: "GstNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_Name",
                table: "Restaurants",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Restaurants_FNumber",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_GstNumber",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_Name",
                table: "Restaurants");
        }
    }
}
