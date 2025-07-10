namespace Application.DTOs;
public class PaymentDto
{
	public int PaymentId { get; set; }
	public DateTime PaymentDate { get; set; }
	public decimal AmountPaid { get; set; }
	public string ModeOfPayment { get; set; } // Cash, UPI, etc.
	public string? Notes { get; set; }
	public int ShopId { get; set; }
	public int OrderId { get; set; }
}