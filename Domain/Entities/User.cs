namespace Domain.Entities;

public class User: BaseEntity
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string ContactNo { get; set; }
	public string Location { get; set; }
	public string Username { get; set; }
	//public string Password { get; set; }
	public byte[] PasswordHash { get; set; }
	public byte[] PasswordSalt { get; set; }
	public string Role { get; set; }
}
