namespace Domain.Entities;

public class MiscExpense
{
	public int Id { get; set; }
	public DateTime Date { get; set; }
	public string Description { get; set; }
	public decimal Amount { get; set; }
}
