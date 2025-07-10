namespace Domain.Entities;

public class Vehicle:BaseEntity
{
	public required string VehicleNumber { get; set; }
	public string Type { get; set; } // Small Van, Truck, etc.
	public double CapacityInKg { get; set; }

	//public ICollection<Delivery> Deliveries { get; set; }
}
