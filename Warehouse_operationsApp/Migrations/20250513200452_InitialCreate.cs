using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse_operationsApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doljnostis",
                columns: table => new
                {
                    id_doljnosti = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doljnostis", x => x.id_doljnosti);
                });

            migrationBuilder.CreateTable(
                name: "Product_Types",
                columns: table => new
                {
                    id_product_type = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Types", x => x.id_product_type);
                });

            migrationBuilder.CreateTable(
                name: "Supplierss",
                columns: table => new
                {
                    id_suppliers = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact_Information = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplierss", x => x.id_suppliers);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    id_unit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.id_unit);
                });

            migrationBuilder.CreateTable(
                name: "Userss",
                columns: table => new
                {
                    id_users = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_doljnosti = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userss", x => x.id_users);
                    table.ForeignKey(
                        name: "FK_Userss_Doljnostis_id_doljnosti",
                        column: x => x.id_doljnosti,
                        principalTable: "Doljnostis",
                        principalColumn: "id_doljnosti",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id_Product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vendor_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    id_product_type = table.Column<int>(type: "int", nullable: false),
                    id_unit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id_Product);
                    table.ForeignKey(
                        name: "FK_Products_Product_Types_id_product_type",
                        column: x => x.id_product_type,
                        principalTable: "Product_Types",
                        principalColumn: "id_product_type",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Units_id_unit",
                        column: x => x.id_unit,
                        principalTable: "Units",
                        principalColumn: "id_unit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipt_And_Expense_Documentss",
                columns: table => new
                {
                    id_doc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiptAndexpense_documents = table.Column<bool>(type: "bit", nullable: false),
                    id_users = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt_And_Expense_Documentss", x => x.id_doc);
                    table.ForeignKey(
                        name: "FK_Receipt_And_Expense_Documentss_Userss_id_users",
                        column: x => x.id_users,
                        principalTable: "Userss",
                        principalColumn: "id_users",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehousess",
                columns: table => new
                {
                    id_warehouses = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_users = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehousess", x => x.id_warehouses);
                    table.ForeignKey(
                        name: "FK_Warehousess_Userss_id_users",
                        column: x => x.id_users,
                        principalTable: "Userss",
                        principalColumn: "id_users",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Information_About_Documentss",
                columns: table => new
                {
                    id_inf_doc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Product = table.Column<int>(type: "int", nullable: false),
                    Quanity = table.Column<int>(type: "int", nullable: false),
                    id_doc = table.Column<int>(type: "int", nullable: false),
                    id_suppliers = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false, computedColumnSql: "[Quanity] * [Cost]", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Information_About_Documentss", x => x.id_inf_doc);
                    table.ForeignKey(
                        name: "FK_Information_About_Documentss_Products_id_Product",
                        column: x => x.id_Product,
                        principalTable: "Products",
                        principalColumn: "id_Product",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Information_About_Documentss_Receipt_And_Expense_Documentss_id_doc",
                        column: x => x.id_doc,
                        principalTable: "Receipt_And_Expense_Documentss",
                        principalColumn: "id_doc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Information_About_Documentss_Supplierss_id_suppliers",
                        column: x => x.id_suppliers,
                        principalTable: "Supplierss",
                        principalColumn: "id_suppliers",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ostatkis",
                columns: table => new
                {
                    id_Ostatki = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_warehouses = table.Column<int>(type: "int", nullable: false),
                    id_Product = table.Column<int>(type: "int", nullable: false),
                    Quantity_Ostatki = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ostatkis", x => x.id_Ostatki);
                    table.ForeignKey(
                        name: "FK_Ostatkis_Products_id_Product",
                        column: x => x.id_Product,
                        principalTable: "Products",
                        principalColumn: "id_Product",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ostatkis_Warehousess_id_warehouses",
                        column: x => x.id_warehouses,
                        principalTable: "Warehousess",
                        principalColumn: "id_warehouses",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Information_About_Documentss_id_doc",
                table: "Information_About_Documentss",
                column: "id_doc");

            migrationBuilder.CreateIndex(
                name: "IX_Information_About_Documentss_id_Product",
                table: "Information_About_Documentss",
                column: "id_Product",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Information_About_Documentss_id_suppliers",
                table: "Information_About_Documentss",
                column: "id_suppliers");

            migrationBuilder.CreateIndex(
                name: "IX_Ostatkis_id_Product",
                table: "Ostatkis",
                column: "id_Product");

            migrationBuilder.CreateIndex(
                name: "IX_Ostatkis_id_warehouses",
                table: "Ostatkis",
                column: "id_warehouses");

            migrationBuilder.CreateIndex(
                name: "IX_Products_id_product_type",
                table: "Products",
                column: "id_product_type");

            migrationBuilder.CreateIndex(
                name: "IX_Products_id_unit",
                table: "Products",
                column: "id_unit");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_And_Expense_Documentss_id_users",
                table: "Receipt_And_Expense_Documentss",
                column: "id_users");

            migrationBuilder.CreateIndex(
                name: "IX_Userss_id_doljnosti",
                table: "Userss",
                column: "id_doljnosti");

            migrationBuilder.CreateIndex(
                name: "IX_Warehousess_id_users",
                table: "Warehousess",
                column: "id_users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Information_About_Documentss");

            migrationBuilder.DropTable(
                name: "Ostatkis");

            migrationBuilder.DropTable(
                name: "Receipt_And_Expense_Documentss");

            migrationBuilder.DropTable(
                name: "Supplierss");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Warehousess");

            migrationBuilder.DropTable(
                name: "Product_Types");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Userss");

            migrationBuilder.DropTable(
                name: "Doljnostis");
        }
    }
}
