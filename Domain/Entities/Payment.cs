namespace Domain.Entities;

public class Payment
{
	public int Id { get; set; }
	public DateTime PaymentDate { get; set; }
	public decimal AmountPaid { get; set; }
	public string ModeOfPayment { get; set; } // Cash, UPI, etc.
	public string? Notes { get; set; }
	public int ShopId { get; set; }
	public Shop Shop { get; set; }
	public int OrderId { get; set; }
	public Order Order { get; set; }
}
