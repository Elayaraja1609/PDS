namespace Domain.Entities;

public class DailyWorkLog : BaseEntity
{
    public DateTime WorkDate { get; set; }
    public int DriverId { get; set; }
    public Driver Driver { get; set; }
    public int? AssistantId { get; set; }
    public Driver? Assistant { get; set; }
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public string Route { get; set; }
    public bool IsPresent { get; set; } = true;
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public double? FuelExpense { get; set; }
    public double? TollCharges { get; set; }
    public double? DailyAllowance { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
