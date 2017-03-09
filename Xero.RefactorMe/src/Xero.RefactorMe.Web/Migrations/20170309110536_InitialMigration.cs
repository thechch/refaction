﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xero.RefactorMe.Web.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeliveryPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOption_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOption_ProductId",
                table: "ProductOption",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOption");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
