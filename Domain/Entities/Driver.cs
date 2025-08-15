namespace Domain.Entities;

public class Driver : BaseEntity
{
	public required string Name { get; set; }
	public required string PhoneNumber { get; set; }
	public string? Address { get; set; }
	public string? LicenseNumber { get; set; }
	public DateTime? LicenseExpiryDate { get; set; }
	public double BaseSalary { get; set; }
	public double DailyRate { get; set; }
	public string Role { get; set; } // Driver, Assistant, or Both
	public bool IsActive { get; set; } = true;
	public DateTime JoinDate { get; set; } = DateTime.UtcNow;
	public DateTime? TerminationDate { get; set; }
	public string? EmergencyContact { get; set; }
	public string? EmergencyContactPhone { get; set; }
	
	// Navigation properties
	public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
	public ICollection<Delivery> AssistantDeliveries { get; set; } = new List<Delivery>();
	public ICollection<DailyWorkLog> WorkLogs { get; set; } = new List<DailyWorkLog>();
	public ICollection<DailyWorkLog> AssistantWorkLogs { get; set; } = new List<DailyWorkLog>();
	public ICollection<Salary> Salaries { get; set; } = new List<Salary>();
	public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
