namespace Domain.Entities;

public class Delivery : BaseEntity
{
	public DateTime DeliveryDate { get; set; }
	public string Area { get; set; }
	public double TotalWeightLoaded { get; set; }
	public double RemainingWeight { get; set; }
	public double TotalAmount { get; set; }
	public double PricePerKg { get; set; }

	public int VehicleId { get; set; }
	public Vehicle Vehicle { get; set; }

	public int DriverId { get; set; }
	public Driver Driver { get; set; }

	public int? AssistantId { get; set; }
	public Driver? Assistant { get; set; }

	public string Route { get; set; }
	public string Status { get; set; } // Pending, InProgress, Completed, Cancelled
	public DateTime? StartTime { get; set; }
	public DateTime? EndTime { get; set; }

	public ICollection<DeliveryDetail> DeliveryDetails { get; set; } = new List<DeliveryDetail>();
	public ICollection<TransportCost> TransportCosts { get; set; } = new List<TransportCost>();
	public ICollection<ChickenBatch> ChickenBatches { get; set; } = new List<ChickenBatch>(); // Track which batches were used
}
