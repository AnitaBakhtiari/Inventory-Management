using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssuanceDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuanceDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductInstances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInstances_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssuanceDocumentProductInstance",
                columns: table => new
                {
                    IssuanceDocumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductInstancesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuanceDocumentProductInstance", x => new { x.IssuanceDocumentsId, x.ProductInstancesId });
                    table.ForeignKey(
                        name: "FK_IssuanceDocumentProductInstance_IssuanceDocuments_IssuanceDocumentsId",
                        column: x => x.IssuanceDocumentsId,
                        principalTable: "IssuanceDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssuanceDocumentProductInstance_ProductInstances_ProductInstancesId",
                        column: x => x.ProductInstancesId,
                        principalTable: "ProductInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssuanceDocumentProductInstance_ProductInstancesId",
                table: "IssuanceDocumentProductInstance",
                column: "ProductInstancesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInstances_ProductId",
                table: "ProductInstances",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssuanceDocumentProductInstance");

            migrationBuilder.DropTable(
                name: "IssuanceDocuments");

            migrationBuilder.DropTable(
                name: "ProductInstances");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
