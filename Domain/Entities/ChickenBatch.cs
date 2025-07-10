namespace Domain.Entities;

public class ChickenBatch: BaseEntity
{
	public DateTime CollectionDate { get; set; }
	public int NumberOfChickens { get; set; }
	public double QuantityInKg { get; set; }
	public string Type { get; set; } // Broiler, Layer, etc.
	public string Status { get; set; } // e.g., Pending, InStock, Dispatched, etc.
	public string? Notes { get; set; }
	public int FarmerId { get; set; }
	public Farm Farmer { get; set; }

	public int FarmStockId { get; set; }
	//public FarmStock FarmStock { get; set; }
}
