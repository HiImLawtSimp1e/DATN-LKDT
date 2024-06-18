using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_relation_bill_billdetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Bill_IdBill",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_IdBill",
                table: "BillDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_IdBill",
                table: "BillDetails",
                column: "IdBill");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Bill_IdBill",
                table: "BillDetails",
                column: "IdBill",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
