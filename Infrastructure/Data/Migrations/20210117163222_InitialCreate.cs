using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductBrands",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockReceive",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    VehicleRegNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    Rejected = table.Column<bool>(type: "bit", nullable: false),
                    Submitted = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrossReference = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Approvedon = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TransactionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Expired = table.Column<bool>(type: "bit", nullable: false),
                    Damaged = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IssueAction = table.Column<bool>(type: "bit", nullable: false),
                    ReturnAction = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveAction = table.Column<bool>(type: "bit", nullable: false),
                    CloseToExpiry = table.Column<bool>(type: "bit", nullable: false),
                    SellableAction = table.Column<bool>(type: "bit", nullable: false),
                    IsSystemDefined = table.Column<bool>(type: "bit", nullable: false),
                    AdjustmentAction = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MeasurementName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTypeId = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    ProductBrandId = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockReceiveDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    BillQuantity = table.Column<int>(type: "int", nullable: false),
                    ReceivedQuantity = table.Column<int>(type: "int", nullable: false),
                    StockReceiveId = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    StockStateId = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Measurement = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    StockStateName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceiveDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReceiveDetail_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockReceiveDetail_StockReceive_StockReceiveId",
                        column: x => x.StockReceiveId,
                        principalTable: "StockReceive",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockReceiveDetail_StockState_StockStateId",
                        column: x => x.StockStateId,
                        principalTable: "StockState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBrandId",
                table: "Products",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveDetail_ProductId",
                table: "StockReceiveDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveDetail_StockReceiveId",
                table: "StockReceiveDetail",
                column: "StockReceiveId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveDetail_StockStateId",
                table: "StockReceiveDetail",
                column: "StockStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockReceiveDetail");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "StockReceive");

            migrationBuilder.DropTable(
                name: "StockState");

            migrationBuilder.DropTable(
                name: "ProductBrands");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
