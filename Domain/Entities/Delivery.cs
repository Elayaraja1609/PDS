namespace Domain.Entities;

public class Delivery:BaseEntity
{
	//public Guid OrderId { get; set; }
	public DateTime DeliveryDate { get; set; }
	public string Area { get; set; }
	public double TotalWeightLoaded { get; set; }
	public double RemainingWeight { get; set; }

	public int VehicleId { get; set; }
	public Vehicle Vehicle { get; set; }

	public int DriverId { get; set; }
	public Driver Driver { get; set; }

	public int FarmStockId { get; set; }
	public FarmStock FarmStock { get; set; }

	public ICollection<Order> Orders { get; set; }
	public ICollection<TransportCost> TransportCosts { get; set; }
}
