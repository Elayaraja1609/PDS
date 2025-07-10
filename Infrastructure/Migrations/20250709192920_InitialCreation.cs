using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiscExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiscExpenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapacityInKg = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChickenBatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfChickens = table.Column<int>(type: "int", nullable: false),
                    QuantityInKg = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    FarmStockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChickenBatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChickenBatches_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FarmStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantityAvailableInKg = table.Column<double>(type: "float", nullable: false),
                    NoOfChickens = table.Column<int>(type: "int", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarmStocks_Farmers_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farmers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalWeightLoaded = table.Column<double>(type: "float", nullable: false),
                    RemainingWeight = table.Column<double>(type: "float", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    FarmStockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deliveries_FarmStocks_FarmStockId",
                        column: x => x.FarmStockId,
                        principalTable: "FarmStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deliveries_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantityInKg = table.Column<double>(type: "float", nullable: false),
                    RatePerKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BalanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    DeliveryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    DeliveryId = table.Column<int>(type: "int", nullable: true),
                    ExpenseType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportCosts_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportCosts_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModeOfPayment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.InsertData(
            //    table: "AppUsers",
            //    columns: new[] { "Id", "ContactNo", "FirstName", "LastName", "Location", "PasswordHash", "PasswordSalt", "Role", "Username" },
            //    values: new object[,]
            //    {
            //        { 1, "+917200806208", "Visu", "", "Marandahalli", new byte[0], new byte[0], "Admin", "admin" },
            //        { 2, "+917200806208", "Visu1", "", "Marandahalli", new byte[0], new byte[0], "Driver", "driver1" }
            //    });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "Name", "PhoneNumber" },
                values: new object[] { 1, "Ravi", "999-888-7777" });

            migrationBuilder.InsertData(
                table: "FarmStocks",
                columns: new[] { "Id", "Date", "FarmId", "NoOfChickens", "QuantityAvailableInKg" },
                values: new object[] { 1, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), null, 250, 500.0 });

            migrationBuilder.InsertData(
                table: "Farmers",
                columns: new[] { "Id", "ContactPerson", "Location", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Ramu", "Brampton", "Ramu", "111-222-3333" },
                    { 2, "Kumar", "Brampton", "Kumar", "222-333-4444" }
                });

            migrationBuilder.InsertData(
                table: "MiscExpenses",
                columns: new[] { "Id", "Amount", "Date", "Description" },
                values: new object[] { 1, 300m, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), "Ice Packing" });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Address", "ContactNumber", "ShopName" },
                values: new object[,]
                {
                    { 1, "Street 1", "444-555-6666", "Fresh Chicken" },
                    { 2, "Street 2", "555-666-7777", "Meat Mart" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CapacityInKg", "Type", "VehicleNumber" },
                values: new object[] { 1, 400.0, "Small Van", "TN01AA1234" });

            migrationBuilder.InsertData(
                table: "ChickenBatches",
                columns: new[] { "Id", "CollectionDate", "FarmStockId", "FarmerId", "Notes", "NumberOfChickens", "QuantityInKg", "Status", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), 1, 1, "", 50, 300.0, "InStock", "Broiler" },
                    { 2, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), 1, 2, "", 40, 200.0, "Dispatched", "Layer" }
                });

            migrationBuilder.InsertData(
                table: "Deliveries",
                columns: new[] { "Id", "Area", "DeliveryDate", "DriverId", "FarmStockId", "RemainingWeight", "TotalWeightLoaded", "VehicleId" },
                values: new object[] { 1, "Market Area", new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), 1, 1, 0.0, 250.0, 1 });

            migrationBuilder.InsertData(
                table: "TransportCosts",
                columns: new[] { "Id", "Amount", "Date", "DeliveryId", "ExpenseType", "Notes", "VehicleId" },
                values: new object[] { 4, 200.00m, new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), null, 2, "Oil change and check-up", 1 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "BalanceAmount", "DeliveryId", "OrderDate", "PaidAmount", "QuantityInKg", "RatePerKg", "ShopId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 0m, 1, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), 20000m, 150.0, 150m, 1, 0m },
                    { 2, 0m, 1, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), 12000m, 100.0, 160m, 2, 0m }
                });

            migrationBuilder.InsertData(
                table: "TransportCosts",
                columns: new[] { "Id", "Amount", "Date", "DeliveryId", "ExpenseType", "Notes", "VehicleId" },
                values: new object[,]
                {
                    { 1, 150.00m, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), 1, 0, "Filled 30L diesel", 1 },
                    { 2, 500.00m, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), 1, 1, "Driver daily wage", 1 },
                    { 3, 70.00m, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), 1, 3, "Highway toll", 1 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "AmountPaid", "ModeOfPayment", "Notes", "OrderId", "PaymentDate", "ShopId" },
                values: new object[,]
                {
                    { 1, 20000m, "Cash", null, 1, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), 1 },
                    { 2, 12000m, "UPI", null, 2, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChickenBatches_FarmerId",
                table: "ChickenBatches",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DriverId",
                table: "Deliveries",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_FarmStockId",
                table: "Deliveries",
                column: "FarmStockId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_VehicleId",
                table: "Deliveries",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmStocks_FarmId",
                table: "FarmStocks",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryId",
                table: "Orders",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShopId",
                table: "Orders",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ShopId",
                table: "Payments",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportCosts_DeliveryId",
                table: "TransportCosts",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportCosts_VehicleId",
                table: "TransportCosts",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "ChickenBatches");

            migrationBuilder.DropTable(
                name: "MiscExpenses");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "TransportCosts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "FarmStocks");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Farmers");
        }
    }
}
