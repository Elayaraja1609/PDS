using Domain.Entities;

namespace Application.DTOs;

public class SalaryDto
{
    public int Id { get; set; }
    public int DriverId { get; set; }
    public string DriverName { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int DaysWorked { get; set; }
    public double BaseSalary { get; set; }
    public double DailyRate { get; set; }
    public double Bonus { get; set; }
    public double TotalSalary { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateSalaryDto
{
    public int DriverId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public double Bonus { get; set; }
    public string? Notes { get; set; }
}

public class PayrollSummaryDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public int TotalStaff { get; set; }
    public double TotalSalary { get; set; }
    public double TotalBonus { get; set; }
    public double TotalPaid { get; set; }
    public double TotalPending { get; set; }
    public List<SalaryDto> Salaries { get; set; } = new List<SalaryDto>();
}
