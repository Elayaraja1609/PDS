using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Common;
namespace Infrastructure.Data;

public static class DataSeeder
{
	public static void Seed(ModelBuilder modelBuilder)
	{
		// 🐔 Farmers
		modelBuilder.Entity<Farm>().HasData(
			new Farm { Id = 1, Name = "Ramu",ContactPerson="Ramu",Location="Brampton", PhoneNumber = "111-222-3333" },
			new Farm { Id = 2, Name = "Kumar", ContactPerson = "Kumar", Location = "Brampton", PhoneNumber = "222-333-4444" }
		);

		// 🏠 Farm Stock
		modelBuilder.Entity<FarmStock>().HasData(
			new FarmStock { Id = 1, Date = DateTime.Today, QuantityAvailableInKg = 500,NoOfChickens=250}
		);

		// 🐥 Chicken Batches
		modelBuilder.Entity<ChickenBatch>().HasData(
			new ChickenBatch { Id = 1, CollectionDate = DateTime.Today, QuantityInKg = 300, Type = "Broiler", Status = "InStock", NumberOfChickens = 50, Notes = "", FarmerId = 1, FarmerName = "Ramu" },
			new ChickenBatch { Id = 2, CollectionDate = DateTime.Today, QuantityInKg = 200, Type = "Layer", Status = "Dispatched", NumberOfChickens = 40, Notes = "", FarmerId = 2, FarmerName = "Kumar" }
		);

		// 🚛 Vehicles
		modelBuilder.Entity<Vehicle>().HasData(
			new Vehicle { Id = 1, VehicleNumber = "TN01AA1234", Type = "Small Van", CapacityInKg = 400, Brand = "Tata", FuelType = "Diesel", Model = "Ace", Status = "Active" }
		);

		// 👨‍✈️ Drivers
		modelBuilder.Entity<Driver>().HasData(
			new Driver { Id = 1, Name = "Ravi", PhoneNumber = "999-888-7777", Role = "Driver" }
		);

		// 🚚 Delivery
		modelBuilder.Entity<Delivery>().HasData(
			new Delivery
			{
				Id = 1,
				DeliveryDate = DateTime.Today,
				Area = "Market Area",
				TotalWeightLoaded = 250,
				RemainingWeight = 0,
				DriverId = 1,
				VehicleId = 1,
				Route = "Default Route",
				Status = "Pending"
				//FarmStockId = 1
			}
		);

		// 🏪 Shops
		modelBuilder.Entity<Shop>().HasData(
			new Shop { Id = 1, ShopName = "Fresh Chicken", ContactNumber = "444-555-6666", Address = "Street 1" },
			new Shop { Id = 2, ShopName = "Meat Mart", ContactNumber = "555-666-7777", Address = "Street 2" }
		);

		// 🛒 Orders
		modelBuilder.Entity<Order>().HasData(
			new Order { Id = 1, OrderDate = DateTime.Today, QuantityInKg = 150, RatePerKg = 150, PaidAmount = 20000, ShopId = 1, DeliveryId = 1 },
			new Order { Id = 2, OrderDate = DateTime.Today, QuantityInKg = 100, RatePerKg = 160, PaidAmount = 12000, ShopId = 2, DeliveryId = 1 }
		);

		// 💰 Payments
		modelBuilder.Entity<Payment>().HasData(
			new Payment { Id = 1, PaymentDate = DateTime.Today, AmountPaid = 20000, ModeOfPayment = "Cash", ShopId = 1,OrderId=1 },
			new Payment { Id = 2, PaymentDate = DateTime.Today, AmountPaid = 12000, ModeOfPayment = "UPI", ShopId = 2, OrderId=2 }
		);

		// 💸 Transport Cost
		modelBuilder.Entity<TransportCost>().HasData(
			new TransportCost {Id = 1,Date = DateTime.Today,VehicleId = 1,DeliveryId = 1,ExpenseType = ExpenseType.Fuel,Amount = 150.00m,Notes = "Filled 30L diesel"},
			new TransportCost {Id = 2,Date = DateTime.Today,VehicleId = 1,DeliveryId = 1,ExpenseType = ExpenseType.DriverWage,Amount = 500.00m,Notes = "Driver daily wage"},
			new TransportCost{Id = 3,Date = DateTime.Today,VehicleId = 1,DeliveryId = 1,ExpenseType = ExpenseType.Toll,Amount = 70.00m,	Notes = "Highway toll"},
			new TransportCost	{Id = 4,Date = DateTime.Today.AddDays(-1),VehicleId = 1,DeliveryId = null,ExpenseType = ExpenseType.Maintenance,Amount = 200.00m,Notes = "Oil change and check-up"}
		);


		// 🧾 Misc Expense
		modelBuilder.Entity<MiscExpense>().HasData(
			new MiscExpense { Id = 1, Date = DateTime.Today, Description = "Ice Packing", Amount = 300 }
		);

		// 🔐 Users
		modelBuilder.Entity<User>().HasData(
			new User { Id = 1, FirstName="Visu",LastName="",ContactNo="+917200806208",Location="Marandahalli", Username = "admin", PasswordHash = new byte[0], PasswordSalt = new byte[0], Role = "Admin" },
			new User { Id = 2, FirstName = "Visu1", LastName = "", ContactNo = "+917200806208", Location = "Marandahalli", Username = "driver1", PasswordHash = new byte[0], PasswordSalt = new byte[0], Role = "Driver" }
		);
	}
}
