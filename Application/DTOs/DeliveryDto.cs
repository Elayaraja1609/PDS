using Domain.Entities;

namespace Application.DTOs;

public class DeliveryDto
{
	public int DeliveryId { get; set; }
	public DateTime DeliveryDate { get; set; }
	public string Area { get; set; }
	public double TotalWeightLoaded { get; set; }
	public double RemainingWeight { get; set; }
	public double TotalAmount { get; set; }
	public double PricePerKg { get; set; }

	public int VehicleId { get; set; }
	//public Vehicle Vehicle { get; set; }

	public int DriverId { get; set; }
	//public Driver Driver { get; set; }

	public int FarmStockId { get; set; }
	//public FarmStock FarmStock { get; set; }
	public int? AssistantId { get; set; }
	public string Route { get; set; }
	public string Status { get; set; } // Pending, InProgress, Completed, Cancelled
	public DateTime? StartTime { get; set; }
	public DateTime? EndTime { get; set; }

	public ICollection<OrderDto>? Orders { get; set; }
	public ICollection<TransportCost>? TransportCosts { get; set; }
	public ICollection<MiscExpenseDto>? MiscExpenses { get; set; }
}
