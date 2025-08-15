namespace Domain.Entities;

public class User : BaseEntity
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string ContactNo { get; set; }
	public string Location { get; set; }
	public string Username { get; set; }
	public byte[] PasswordHash { get; set; }
	public byte[] PasswordSalt { get; set; }
	public string Role { get; set; } // Admin, Staff, Driver
	public bool IsActive { get; set; } = true;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime? LastLoginAt { get; set; }
	public string? Email { get; set; }
	public string? ProfilePicture { get; set; }
	
	// Navigation properties
	public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
