namespace Domain.Entities;

public class ChickenBatch : BaseEntity
{
	public DateTime CollectionDate { get; set; }
	public int NumberOfChickens { get; set; }
	public double QuantityInKg { get; set; }
	public double RemainingQuantityInKg { get; set; } // Track remaining quantity for FIFO
	public string Type { get; set; } // Broiler, Layer, etc.
	public string Status { get; set; } // e.g., Pending, InStock, Dispatched, etc.
	public string? Notes { get; set; }
	public int FarmerId { get; set; }
	public Farm Farmer { get; set; }
	public string FarmerName { get; set; } // Store farmer name for quick access
	public double CollectionWeight { get; set; } // Weight at collection time
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // For FIFO ordering
	public bool IsActive { get; set; } = true; // Track if batch is still active
}
