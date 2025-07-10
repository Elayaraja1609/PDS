
using Application.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Services;

public class UserService : IUserService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IConfiguration _config;

	public UserService(IUnitOfWork unitOfWork, IConfiguration config)
	{
		_unitOfWork = unitOfWork;
		_config = config;
	}

	public async Task<User?> GetUserByIdAsync(int id)
	{
		return await _unitOfWork.AppUsers.GetByIdAsync(id);
	}

	public async Task<User?> GetUserByUsernameAsync(string username)
	{
		var users = await _unitOfWork.AppUsers.GetAllAsync();
		return users
			.Where(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase))
			.FirstOrDefault();
	}

	public async Task<bool> UserExistsAsync(string username)
	{
		var users = await _unitOfWork.AppUsers.GetAllAsync();
		var user = users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
		if (user == null)
		{
			return false;
		}
		return _unitOfWork.AppUsers.IsExist(user.Id);
	}

	public async Task<User> RegisterAsync(string Firstname, string Lastname, string Mobile, string Location, string username, string password, string role)
	{
		if (await UserExistsAsync(username))
			throw new Exception("Username already exists");

		using var hmac = new HMACSHA512();
		var user = new User
		{
			FirstName= Firstname,
			LastName=Lastname,
			ContactNo=Mobile,
			Location=Location,
			Username = username.ToLower(),
			PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
			PasswordSalt = hmac.Key,
			Role = role
		};

		_unitOfWork.AppUsers.AddAsync(user);
		await _unitOfWork.CompleteAsync();

		return user;
	}

	public async Task<User?> LoginAsync(string username, string password)
	{
		var user = await GetUserByUsernameAsync(username);
		if (user == null) return null;

		using var hmac = new HMACSHA512(user.PasswordSalt);
		var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

		for (int i = 0; i < computedHash.Length; i++)
		{
			if (computedHash[i] != user.PasswordHash[i])
				return null;
		}

		return user;
	}

	public Task<string> GenerateJwtTokenAsync(User user)
	{
		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.Role, user.Role)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

		var token = new JwtSecurityToken(
			issuer: _config["JwtSettings:Issuer"],
			claims: claims,
			expires: DateTime.Now.AddDays(7),
			signingCredentials: creds
		);

		return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
	}
}
