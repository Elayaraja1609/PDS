using Domain.Entities;

namespace Application.DTOs;

public class ExpenseDto
{
    public int Id { get; set; }
    public DateTime ExpenseDate { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public string Category { get; set; }
    public int? VehicleId { get; set; }
    public string? VehicleNumber { get; set; }
    public int? DriverId { get; set; }
    public string? DriverName { get; set; }
    public string? ReceiptNumber { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}

public class CreateExpenseDto
{
    public DateTime ExpenseDate { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public string Category { get; set; }
    public int? VehicleId { get; set; }
    public int? DriverId { get; set; }
    public string? ReceiptNumber { get; set; }
    public string? Notes { get; set; }
}
