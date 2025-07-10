namespace Domain.Entities;

public class FarmStock:BaseEntity
{
	public DateTime Date { get; set; }
	public double QuantityAvailableInKg { get; set; }
	//public double RemainingAvailableInKg { get; set; }
	public int NoOfChickens { get; set; }

	//public ICollection<ChickenBatch> ChickenBatches { get; set; }
	//public ICollection<Delivery> Deliveries { get; set; }
}
