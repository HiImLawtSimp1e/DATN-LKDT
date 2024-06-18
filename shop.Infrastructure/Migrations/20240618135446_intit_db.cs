using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intit_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Bill_BillsId",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_BillsId",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "BillsId",
                table: "BillDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "BillEntityId",
                table: "BillDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserTypeEnum = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaVoucher = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    NameVoucher = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DieuKien = table.Column<int>(type: "int", nullable: false),
                    TypeVoucher = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    MucUuDai = table.Column<double>(type: "float", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusVoucher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefundEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerrialId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MetadataJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDetail = table.Column<int>(type: "int", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VirtualItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdAccount = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReducedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VirtualItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParenId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VirtualType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Isdeleted = table.Column<bool>(type: "bit", nullable: true),
                    Ispublish = table.Column<bool>(type: "bit", nullable: true),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetadataJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Solid = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAccount = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdVirtualItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    MetadataJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartDetails_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillEntityId",
                table: "BillDetails",
                column: "BillEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_IdBill",
                table: "BillDetails",
                column: "IdBill");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_IdDiscount",
                table: "BillDetails",
                column: "IdDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_IdAccount",
                table: "Bill",
                column: "IdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Address_IdAccount",
                table: "Address",
                column: "IdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CartId",
                table: "CartDetails",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Accounts_IdAccount",
                table: "Address",
                column: "IdAccount",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Accounts_IdAccount",
                table: "Bill",
                column: "IdAccount",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Bill_BillEntityId",
                table: "BillDetails",
                column: "BillEntityId",
                principalTable: "Bill",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Bill_IdBill",
                table: "BillDetails",
                column: "IdBill",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Discount_IdDiscount",
                table: "BillDetails",
                column: "IdDiscount",
                principalTable: "Discount",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Accounts_IdAccount",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Accounts_IdAccount",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Bill_BillEntityId",
                table: "BillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Bill_IdBill",
                table: "BillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Discount_IdDiscount",
                table: "BillDetails");

            migrationBuilder.DropTable(
                name: "CartDetails");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "RefundEntity");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "VirtualItem");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_BillEntityId",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_IdBill",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_IdDiscount",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_Bill_IdAccount",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Address_IdAccount",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "BillEntityId",
                table: "BillDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "BillsId",
                table: "BillDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillsId",
                table: "BillDetails",
                column: "BillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Bill_BillsId",
                table: "BillDetails",
                column: "BillsId",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
