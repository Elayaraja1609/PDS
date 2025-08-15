namespace Domain.Entities;

public class Vehicle : BaseEntity
{
	public required string VehicleNumber { get; set; }
	public string Type { get; set; } // Small Van, Truck, etc.
	public double CapacityInKg { get; set; }
	public string Brand { get; set; }
	public string Model { get; set; }
	public int Year { get; set; }
	public string FuelType { get; set; } // Petrol, Diesel, Electric
	public double FuelEfficiency { get; set; } // km/l or km/kWh
	public DateTime? LastMaintenanceDate { get; set; }
	public DateTime? NextMaintenanceDate { get; set; }
	public double CurrentMileage { get; set; }
	public string Status { get; set; } // Active, Maintenance, Retired
	public bool IsActive { get; set; } = true;
	public DateTime PurchaseDate { get; set; }
	public double PurchasePrice { get; set; }
	public string? InsuranceNumber { get; set; }
	public DateTime? InsuranceExpiryDate { get; set; }
	
	// Navigation properties
	public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
	public ICollection<DailyWorkLog> WorkLogs { get; set; } = new List<DailyWorkLog>();
	public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
