using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
	Task<User?> GetUserByIdAsync(int id);
	Task<User?> GetUserByUsernameAsync(string username);
	Task<bool> UserExistsAsync(string username);
	Task<User> RegisterAsync(string Firstname, string Lastname, string Mobile, string Location, string username, string password, string role);
	Task<User?> LoginAsync(string username, string password);
	Task<string> GenerateJwtTokenAsync(User user);
}
