using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_FarmStocks_FarmStockId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportCosts_Vehicles_VehicleId",
                table: "TransportCosts");

            migrationBuilder.DropIndex(
                name: "IX_TransportCosts_VehicleId",
                table: "TransportCosts");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_FarmStockId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "FarmStockId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "FarmStockId",
                table: "ChickenBatches");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "CurrentMileage",
                table: "Vehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FuelEfficiency",
                table: "Vehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "FuelType",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "InsuranceExpiryDate",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsuranceNumber",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMaintenanceDate",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextMaintenanceDate",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Vehicles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "PurchasePrice",
                table: "Vehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BaseSalary",
                table: "Drivers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DailyRate",
                table: "Drivers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContact",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactPhone",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Drivers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDate",
                table: "Drivers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LicenseExpiryDate",
                table: "Drivers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TerminationDate",
                table: "Drivers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssistantId",
                table: "Deliveries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Deliveries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PricePerKg",
                table: "Deliveries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Route",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Deliveries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TotalAmount",
                table: "Deliveries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CollectionWeight",
                table: "ChickenBatches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ChickenBatches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeliveryId",
                table: "ChickenBatches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FarmerName",
                table: "ChickenBatches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ChickenBatches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "RemainingQuantityInKg",
                table: "ChickenBatches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AppUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAt",
                table: "AppUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DailyWorkLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    AssistantId = table.Column<int>(type: "int", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    FuelExpense = table.Column<double>(type: "float", nullable: true),
                    TollCharges = table.Column<double>(type: "float", nullable: true),
                    DailyAllowance = table.Column<double>(type: "float", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyWorkLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyWorkLogs_Drivers_AssistantId",
                        column: x => x.AssistantId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyWorkLogs_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyWorkLogs_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    QuantityDelivered = table.Column<double>(type: "float", nullable: false),
                    PricePerKg = table.Column<double>(type: "float", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryDetails_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryDetails_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryUpdates",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    DaysWorked = table.Column<int>(type: "int", nullable: false),
                    BaseSalary = table.Column<double>(type: "float", nullable: false),
                    DailyRate = table.Column<double>(type: "float", nullable: false),
                    Bonus = table.Column<double>(type: "float", nullable: false),
                    TotalSalary = table.Column<double>(type: "float", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salaries_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "IsActive", "LastLoginAt", "ProfilePicture" },
                values: new object[] { new DateTime(2025, 8, 13, 15, 12, 18, 907, DateTimeKind.Utc).AddTicks(2679), null, true, null, null });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Email", "IsActive", "LastLoginAt", "ProfilePicture" },
                values: new object[] { new DateTime(2025, 8, 13, 15, 12, 18, 907, DateTimeKind.Utc).AddTicks(2682), null, true, null, null });

            migrationBuilder.UpdateData(
                table: "ChickenBatches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CollectionDate", "CollectionWeight", "CreatedAt", "DeliveryId", "FarmerName", "IsActive", "RemainingQuantityInKg" },
                values: new object[] { new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local), 0.0, new DateTime(2025, 8, 13, 15, 12, 18, 907, DateTimeKind.Utc).AddTicks(2399), null, "Ramu", true, 0.0 });

            migrationBuilder.UpdateData(
                table: "ChickenBatches",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CollectionDate", "CollectionWeight", "CreatedAt", "DeliveryId", "FarmerName", "IsActive", "RemainingQuantityInKg" },
                values: new object[] { new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local), 0.0, new DateTime(2025, 8, 13, 15, 12, 18, 907, DateTimeKind.Utc).AddTicks(2407), null, "Kumar", true, 0.0 });

            migrationBuilder.UpdateData(
                table: "Deliveries",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AssistantId", "DeliveryDate", "EndTime", "PricePerKg", "Route", "StartTime", "Status", "TotalAmount" },
                values: new object[] { null, new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local), null, 0.0, "Default Route", null, "Pending", 0.0 });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "BaseSalary", "DailyRate", "EmergencyContact", "EmergencyContactPhone", "IsActive", "JoinDate", "LicenseExpiryDate", "LicenseNumber", "Role", "TerminationDate" },
                values: new object[] { null, 0.0, 0.0, null, null, true, new DateTime(2025, 8, 13, 15, 12, 18, 907, DateTimeKind.Utc).AddTicks(2449), null, null, "Driver", null });

            migrationBuilder.UpdateData(
                table: "FarmStocks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MiscExpenses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TransportCosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TransportCosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TransportCosts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TransportCosts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Brand", "CurrentMileage", "FuelEfficiency", "FuelType", "InsuranceExpiryDate", "InsuranceNumber", "IsActive", "LastMaintenanceDate", "Model", "NextMaintenanceDate", "PurchaseDate", "PurchasePrice", "Status", "Year" },
                values: new object[] { "Tata", 0.0, 0.0, "Diesel", null, null, true, null, "Ace", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "Active", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_AssistantId",
                table: "Deliveries",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenBatches_DeliveryId",
                table: "ChickenBatches",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyWorkLogs_AssistantId",
                table: "DailyWorkLogs",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyWorkLogs_DriverId",
                table: "DailyWorkLogs",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyWorkLogs_VehicleId",
                table: "DailyWorkLogs",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetails_DeliveryId",
                table: "DeliveryDetails",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetails_ShopId",
                table: "DeliveryDetails",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_DriverId",
                table: "Expenses",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VehicleId",
                table: "Expenses",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_DriverId",
                table: "Salaries",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChickenBatches_Deliveries_DeliveryId",
                table: "ChickenBatches",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Drivers_AssistantId",
                table: "Deliveries",
                column: "AssistantId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChickenBatches_Deliveries_DeliveryId",
                table: "ChickenBatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Drivers_AssistantId",
                table: "Deliveries");

            migrationBuilder.DropTable(
                name: "DailyWorkLogs");

            migrationBuilder.DropTable(
                name: "DeliveryDetails");

            migrationBuilder.DropTable(
                name: "DeliveryUpdates");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_AssistantId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_ChickenBatches_DeliveryId",
                table: "ChickenBatches");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CurrentMileage",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "FuelEfficiency",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "InsuranceExpiryDate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "InsuranceNumber",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "LastMaintenanceDate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "NextMaintenanceDate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "BaseSalary",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "DailyRate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "EmergencyContact",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "EmergencyContactPhone",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "LicenseExpiryDate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "TerminationDate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "AssistantId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "PricePerKg",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Route",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "CollectionWeight",
                table: "ChickenBatches");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ChickenBatches");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "ChickenBatches");

            migrationBuilder.DropColumn(
                name: "FarmerName",
                table: "ChickenBatches");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ChickenBatches");

            migrationBuilder.DropColumn(
                name: "RemainingQuantityInKg",
                table: "ChickenBatches");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "LastLoginAt",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AppUsers");

            migrationBuilder.AddColumn<int>(
                name: "FarmStockId",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FarmStockId",
                table: "ChickenBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ChickenBatches",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CollectionDate", "FarmStockId" },
                values: new object[] { new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.UpdateData(
                table: "ChickenBatches",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CollectionDate", "FarmStockId" },
                values: new object[] { new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.UpdateData(
                table: "Deliveries",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeliveryDate", "FarmStockId" },
                values: new object[] { new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.UpdateData(
                table: "FarmStocks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "MiscExpenses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TransportCosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TransportCosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TransportCosts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TransportCosts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Date",
                value: new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_TransportCosts_VehicleId",
                table: "TransportCosts",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_FarmStockId",
                table: "Deliveries",
                column: "FarmStockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_FarmStocks_FarmStockId",
                table: "Deliveries",
                column: "FarmStockId",
                principalTable: "FarmStocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportCosts_Vehicles_VehicleId",
                table: "TransportCosts",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
