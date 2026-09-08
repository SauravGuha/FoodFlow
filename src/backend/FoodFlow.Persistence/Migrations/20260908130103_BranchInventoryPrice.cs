using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BranchInventoryPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BranchInventory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "BranchInventory");
        }
    }
}
