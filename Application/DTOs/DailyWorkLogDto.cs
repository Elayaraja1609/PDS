using Domain.Entities;

namespace Application.DTOs;

public class DailyWorkLogDto
{
    public int Id { get; set; }
    public DateTime WorkDate { get; set; }
    public int DriverId { get; set; }
    public string DriverName { get; set; }
    public int? AssistantId { get; set; }
    public string? AssistantName { get; set; }
    public int VehicleId { get; set; }
    public string VehicleNumber { get; set; }
    public string Route { get; set; }
    public bool IsPresent { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public double? FuelExpense { get; set; }
    public double? TollCharges { get; set; }
    public double? DailyAllowance { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateDailyWorkLogDto
{
    public DateTime WorkDate { get; set; }
    public int DriverId { get; set; }
    public int? AssistantId { get; set; }
    public int VehicleId { get; set; }
    public string Route { get; set; }
    public bool IsPresent { get; set; } = true;
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public double? FuelExpense { get; set; }
    public double? TollCharges { get; set; }
    public double? DailyAllowance { get; set; }
    public string? Notes { get; set; }
}
