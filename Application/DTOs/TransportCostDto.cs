using Domain.Common;

namespace Application.DTOs;

public class TransportCostDto
{
	public int Id { get; set; }
	public DateTime Date { get; set; }
	public int VehicleId { get; set; }
	public int? DeliveryId { get; set; }
	public ExpenseType ExpenseType { get; set; }
	public decimal Amount { get; set; }
	public string? Notes { get; set; }
}
