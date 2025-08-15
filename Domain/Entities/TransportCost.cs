using Domain.Common;

namespace Domain.Entities;

public class TransportCost 
{
	public int Id { get; set; }
	public DateTime Date { get; set; }

	public int VehicleId { get; set; }
	//public Vehicle? Vehicle { get; set; }

	public int? DeliveryId { get; set; } // Optional if tied to a specific delivery
	//public Delivery? Delivery { get; set; }

	public ExpenseType ExpenseType { get; set; }
	public decimal Amount { get; set; }

	public string? Notes { get; set; }
}
