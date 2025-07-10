namespace Application.DTOs;

public class FarmStockDto
{
	public int FarmStockId { get; set; }
	public DateTime Date { get; set; }
	public double QuantityAvailableInKg { get; set; }
	//public double RemainingAvailableInKg { get; set; }
	public int NoOfChickens { get; set; }
}
