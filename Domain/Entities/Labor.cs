namespace Domain.Entities;

public class Labor:BaseEntity
{
	public required string Name { get; set; }
	public required string Role { get; set; } // Loader, Cleaner
	public bool IsAvailable { get; set; }
}
