namespace Domain.Entities;

public class Expense : BaseEntity
{
    public DateTime ExpenseDate { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public string Category { get; set; } // Fuel, Maintenance, Toll, Allowance, etc.
    public int? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public int? DriverId { get; set; }
    public Driver? Driver { get; set; }
    public string? ReceiptNumber { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; }
}
