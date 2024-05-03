using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    idBuyer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuyerName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BuyerPhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.idBuyer);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    idManufacturer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ManufacturerName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Manufactureaddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.idManufacturer);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    idSupplier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SupplierName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.idSupplier);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    idWarehouse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WarehouseName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehouseAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehousePhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.idWarehouse);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    idGoods = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GoodsName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsType = table.Column<int>(type: "int", nullable: true),
                    GoodsPrice = table.Column<int>(type: "int", nullable: true),
                    idManufacturer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.idGoods);
                    table.ForeignKey(
                        name: "FK_Goods_Manufacturers_idManufacturer",
                        column: x => x.idManufacturer,
                        principalTable: "Manufacturers",
                        principalColumn: "idManufacturer",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GoodsParams",
                columns: table => new
                {
                    idGoodsParam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idGoods = table.Column<int>(type: "int", nullable: false),
                    GoodsParamName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsParamValue = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsParamMeasure = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsParams", x => x.idGoodsParam);
                    table.ForeignKey(
                        name: "FK_GoodsParams_Goods_idGoods",
                        column: x => x.idGoods,
                        principalTable: "Goods",
                        principalColumn: "idGoods",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    idStorage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idWarehouse = table.Column<int>(type: "int", nullable: false),
                    idGoods = table.Column<int>(type: "int", nullable: false),
                    StorageCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.idStorage);
                    table.ForeignKey(
                        name: "FK_Storages_Goods_idGoods",
                        column: x => x.idGoods,
                        principalTable: "Goods",
                        principalColumn: "idGoods",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Storages_Warehouses_idWarehouse",
                        column: x => x.idWarehouse,
                        principalTable: "Warehouses",
                        principalColumn: "idWarehouse",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    idTransaction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransactionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    idGoods = table.Column<int>(type: "int", nullable: false),
                    TransactionCount = table.Column<int>(type: "int", nullable: true),
                    idSupplier = table.Column<int>(type: "int", nullable: false),
                    idWarehouseSender = table.Column<int>(type: "int", nullable: false),
                    idWarehouseReceiver = table.Column<int>(type: "int", nullable: false),
                    idBuyer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.idTransaction);
                    table.ForeignKey(
                        name: "FK_Transactions_Buyers_idBuyer",
                        column: x => x.idBuyer,
                        principalTable: "Buyers",
                        principalColumn: "idBuyer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Goods_idGoods",
                        column: x => x.idGoods,
                        principalTable: "Goods",
                        principalColumn: "idGoods",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Suppliers_idSupplier",
                        column: x => x.idSupplier,
                        principalTable: "Suppliers",
                        principalColumn: "idSupplier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Warehouses_idWarehouseReceiver",
                        column: x => x.idWarehouseReceiver,
                        principalTable: "Warehouses",
                        principalColumn: "idWarehouse",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Warehouses_idWarehouseSender",
                        column: x => x.idWarehouseSender,
                        principalTable: "Warehouses",
                        principalColumn: "idWarehouse",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_idManufacturer",
                table: "Goods",
                column: "idManufacturer");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsParams_idGoods",
                table: "GoodsParams",
                column: "idGoods");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_idGoods",
                table: "Storages",
                column: "idGoods");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_idWarehouse",
                table: "Storages",
                column: "idWarehouse");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_idBuyer",
                table: "Transactions",
                column: "idBuyer");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_idGoods",
                table: "Transactions",
                column: "idGoods");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_idSupplier",
                table: "Transactions",
                column: "idSupplier");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_idWarehouseReceiver",
                table: "Transactions",
                column: "idWarehouseReceiver");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_idWarehouseSender",
                table: "Transactions",
                column: "idWarehouseSender");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsParams");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Buyers");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
