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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		DataSeeder.Seed(modelBuilder);
		//modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
	}
}
