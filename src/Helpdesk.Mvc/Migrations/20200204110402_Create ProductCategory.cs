using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Helpdesk.Mvc.Migrations
{
    public partial class CreateProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "productCategoryId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    productCategoryId = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    categoryName = table.Column<string>(maxLength: 100, nullable: false),
                    description = table.Column<string>(maxLength: 200, nullable: true),
                    organizationId = table.Column<Guid>(nullable: false),
                    thumbUrl = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.productCategoryId);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Organization_organizationId",
                        column: x => x.organizationId,
                        principalTable: "Organization",
                        principalColumn: "organizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_productCategoryId",
                table: "Product",
                column: "productCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_organizationId",
                table: "ProductCategory",
                column: "organizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_productCategoryId",
                table: "Product",
                column: "productCategoryId",
                principalTable: "ProductCategory",
                principalColumn: "productCategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_productCategoryId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_Product_productCategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "productCategoryId",
                table: "Product");
        }
    }
}
