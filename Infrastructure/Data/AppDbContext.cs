using Application.DTOs;
using Application.DTOs.ResponsesDto.cs;
using Domain.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Farm> Farmers { get; set; }
	public DbSet<ChickenBatch> ChickenBatches { get; set; }
	public DbSet<FarmStock> FarmStocks { get; set; }
	public DbSet<Vehicle> Vehicles { get; set; }
	public DbSet<Driver> Drivers { get; set; }
	public DbSet<Delivery> Deliveries { get; set; }
	public DbSet<Shop> Shops { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<TransportCost> TransportCosts { get; set; }
	public DbSet<MiscExpense> MiscExpenses { get; set; }
	public DbSet<User> AppUsers { get; set; }
	public DbSet<DeliveryDetail> DeliveryDetails { get; set; } = null!;
	public DbSet<DeliveryUpdatesResDto> DeliveryUpdates { get; set; } = null!;
	
	// New entities
	public DbSet<DailyWorkLog> DailyWorkLogs { get; set; } = null!;
	public DbSet<Expense> Expenses { get; set; } = null!;
	public DbSet<Salary> Salaries { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Configure DeliveryDetail as a proper entity
		modelBuilder.Entity<DeliveryDetail>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.HasOne(e => e.Delivery)
				.WithMany(d => d.DeliveryDetails)
				.HasForeignKey(e => e.DeliveryId);
			entity.HasOne(e => e.Shop)
				.WithMany()
				.HasForeignKey(e => e.ShopId);
		});

		// Configure DailyWorkLog
		modelBuilder.Entity<DailyWorkLog>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.HasOne(e => e.Driver)
				.WithMany(d => d.WorkLogs)
				.HasForeignKey(e => e.DriverId);
			entity.HasOne(e => e.Assistant)
				.WithMany(d => d.AssistantWorkLogs)
				.HasForeignKey(e => e.AssistantId);
			entity.HasOne(e => e.Vehicle)
				.WithMany(v => v.WorkLogs)
				.HasForeignKey(e => e.VehicleId);
		});

		// Configure Expense
		modelBuilder.Entity<Expense>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.HasOne(e => e.Vehicle)
				.WithMany(v => v.Expenses)
				.HasForeignKey(e => e.VehicleId);
			entity.HasOne(e => e.Driver)
				.WithMany(d => d.Expenses)
				.HasForeignKey(e => e.DriverId);
		});

		// Configure Salary
		modelBuilder.Entity<Salary>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.HasOne(e => e.Driver)
				.WithMany(d => d.Salaries)
				.HasForeignKey(e => e.DriverId);
		});

		// Configure ChickenBatch
		modelBuilder.Entity<ChickenBatch>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.HasOne(e => e.Farmer)
				.WithMany()
				.HasForeignKey(e => e.FarmerId);
		});

		// Configure Delivery
		modelBuilder.Entity<Delivery>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.HasOne(e => e.Vehicle)
				.WithMany(v => v.Deliveries)
				.HasForeignKey(e => e.VehicleId);
			entity.HasOne(e => e.Driver)
				.WithMany(d => d.Deliveries)
				.HasForeignKey(e => e.DriverId);
			entity.HasOne(e => e.Assistant)
				.WithMany(d => d.AssistantDeliveries)
				.HasForeignKey(e => e.AssistantId);
		});

		// Configure Driver
		modelBuilder.Entity<Driver>(entity =>
		{
			entity.HasKey(e => e.Id);
		});

		// Configure Vehicle
		modelBuilder.Entity<Vehicle>(entity =>
		{
			entity.HasKey(e => e.Id);
		});

		// Configure User
		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.Id);
		});

		// Keep the existing configuration for DeliveryUpdatesResDto
		modelBuilder.Entity<DeliveryUpdatesResDto>().HasNoKey();

		base.OnModelCreating(modelBuilder);

		DataSeeder.Seed(modelBuilder);
		//modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
	}
}
