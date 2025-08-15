namespace Application.DTOs;

public class MiscExpenseDto
{
	public int MiscExpenseId { get; set; }
	public DateTime Date { get; set; }
	public int DeliveryId { get; set; }
	public string Description { get; set; }
	public decimal Amount { get; set; }
}
