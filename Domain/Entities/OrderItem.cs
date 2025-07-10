namespace Domain.Entities;

public class OrderItem : BaseEntity
{
	public int OrderId { get; set; }
	public Order Order { get; set; }

	public string ProductType { get; set; } // Eggs, Broiler, Layer
	public double QuantityKg { get; set; }
}
