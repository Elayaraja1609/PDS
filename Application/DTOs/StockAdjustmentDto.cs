namespace Application.DTOs;
public class StockAdjustmentDto
{
	public double Quantity { get; set; }
	public bool Increase { get; set; }// true = add, false = subtract
}