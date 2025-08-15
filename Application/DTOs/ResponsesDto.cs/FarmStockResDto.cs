namespace Application.DTOs.ResponsesDto.cs;

public class FarmStockResDto
{
	public int Id { get; set; }
	public DateTime Date { get; set; }
	public double QuantityAvailableInKg { get; set; }
	public int NoOfChickens { get; set; }
}
