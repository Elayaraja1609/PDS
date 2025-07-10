namespace Application.DTOs;
public class OrderDto
{
	public int OrderId { get; set; }
	public DateTime OrderDate { get; set; }
	public double QuantityInKg { get; set; }
	public decimal RatePerKg { get; set; }
	public decimal TotalAmount { get; set; }
	public decimal PaidAmount { get; set; }
	public decimal BalanceAmount { get; set; }

	public int ShopId { get; set; }
}
