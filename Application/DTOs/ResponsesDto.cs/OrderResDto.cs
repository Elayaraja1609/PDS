using Domain.Entities;

namespace Application.DTOs.ResponsesDto.cs;

public class OrderResDto
{
	public int Id { get; set; }
	public DateTime OrderDate { get; set; }
	public double QuantityInKg { get; set; }
	public decimal RatePerKg { get; set; }
	public decimal TotalAmount { get; set; }
	public decimal PaidAmount { get; set; }
	public decimal BalanceAmount { get; set; }

	public int ShopId { get; set; }
	public ShopResDto? Shop { get; set; }

}
