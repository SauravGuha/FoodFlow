using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OrderExternalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentGateWayId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentGateWayId",
                table: "Order");
        }
    }
}
