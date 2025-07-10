namespace Domain.Entities;

public class Driver:BaseEntity
{
	public required string Name { get; set; }
	public required string PhoneNumber { get; set; }

	//public ICollection<Delivery> Deliveries { get; set; }
}
