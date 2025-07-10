namespace Domain.Entities;

public class Shop:BaseEntity
{
	public string ShopName { get; set; }
	public string ContactNumber { get; set; }
	public string Address { get; set; }

	//public ICollection<Order> Orders { get; set; }
	//public ICollection<Payment> Payments { get; set; }
}
