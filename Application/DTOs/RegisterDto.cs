namespace Application.DTOs;

public class RegisterDto
{
	public required string Firstname { get; set; }
	public required string Lastname { get; set; }
	public required string ContactNo { get; set; }
	public required string Location { get; set; }
	public required string Username { get; set; }
	public required string Password { get; set; }
	public required string Role { get; set; }
}
