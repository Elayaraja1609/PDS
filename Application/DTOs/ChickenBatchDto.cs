using Domain.Entities;

namespace Application.DTOs;

public class ChickenBatchDto
{
	public int Id { get; set; }
	public DateTime CollectionDate { get; set; }
	public int NumberOfChickens { get; set; }
	public double QuantityInKg { get; set; }
	public string Type { get; set; } // Broiler, Layer, etc.
	public string? Notes { get; set; }
	public string Status { get; set; } // e.g., Pending, InStock, Dispatched, etc.
	public int FarmerId { get; set; }
	public FarmDto? Farm { get; set; } 
	public int FarmStockId { get; set; }
	//public FarmStockDto? FarmStock { get; set; }
}
