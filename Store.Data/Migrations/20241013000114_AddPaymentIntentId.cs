using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentIntentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductName",
                table: "orderItems",
                newName: "ProductItem_ProductName");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_ProductItemId",
                table: "orderItems",
                newName: "ProductItem_ProductItemId");

            migrationBuilder.RenameColumn(
                name: "ItemOrdered_PictureUrl",
                table: "orderItems",
                newName: "ProductItem_PictureUrl");

            migrationBuilder.AddColumn<string>(
                name: "BasketId",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "ProductItem_ProductName",
                table: "orderItems",
                newName: "ItemOrdered_ProductName");

            migrationBuilder.RenameColumn(
                name: "ProductItem_ProductItemId",
                table: "orderItems",
                newName: "ItemOrdered_ProductItemId");

            migrationBuilder.RenameColumn(
                name: "ProductItem_PictureUrl",
                table: "orderItems",
                newName: "ItemOrdered_PictureUrl");
        }
    }
}
