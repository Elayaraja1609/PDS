namespace Domain.Entities;

public class Salary : BaseEntity
{
    public int DriverId { get; set; }
    public Driver Driver { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int DaysWorked { get; set; }
    public double BaseSalary { get; set; }
    public double DailyRate { get; set; }
    public double Bonus { get; set; }
    public double TotalSalary { get; set; }
    public bool IsPaid { get; set; } = false;
    public DateTime? PaymentDate { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
