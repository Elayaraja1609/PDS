namespace Domain.Entities;

public class InventoryItem 
{
	public int Id { get; set; }
	public int FarmId { get; set; }
	public required Farm Farm { get; set; }

	public DateTime ReceivedDate { get; set; }
	public double QuantityKg { get; set; }
	public string? BatchCode { get; set; }
	public DateTime ExpiryDate { get; set; }

	public string? Status { get; set; } // e.g., Fresh, Spoiled, Sold
}
