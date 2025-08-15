namespace Domain.Entities;

public class Order
{
	public int Id { get; set; }
	public DateTime OrderDate { get; set; }
	public double QuantityInKg { get; set; }
	public decimal RatePerKg { get; set; }
	public decimal TotalAmount { get; set; }
	public decimal PaidAmount { get; set; }
	public decimal BalanceAmount { get; set; }

	public int ShopId { get; set; }
	public Shop Shop { get; set; }

	public int DeliveryId { get; set; }
	public Delivery Delivery { get; set; }
}
