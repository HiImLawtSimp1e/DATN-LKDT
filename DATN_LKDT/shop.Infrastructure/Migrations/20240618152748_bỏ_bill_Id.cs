﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bỏ_bill_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdBill",
                table: "BillDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdBill",
                table: "BillDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
