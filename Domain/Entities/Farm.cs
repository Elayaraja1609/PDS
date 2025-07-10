namespace Domain.Entities;

public class Farm:BaseEntity
{
	public required string Name { get; set; }
	public required string Location { get; set; }
	public required string ContactPerson { get; set; }
	public required string PhoneNumber { get; set; }

	//public ICollection<ChickenBatch> ChickenBatches { get; set; }
	public ICollection<FarmStock> FarmStocks { get; set; } = new List<FarmStock>();
}
