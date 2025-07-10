using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class UserController(IUserService userService) : BaseApiController
	{
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto dto)
		{
			try
			{
				var user = await userService.RegisterAsync(dto.Firstname,dto.Lastname,dto.ContactNo,dto.Location,dto.Username, dto.Password, dto.Role);
				return Ok(new
				{
					user.Id,
					user.Username,
					user.Role
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			var user = await userService.LoginAsync(dto.Username, dto.Password);
			if (user == null)
				return Unauthorized("Invalid credentials");

			var token = await userService.GenerateJwtTokenAsync(user);

			return Ok(new
			{
				token,
				user = new { user.Id, user.Username, user.Role }
			});
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetById(int id)
		{
			var user = await userService.GetUserByIdAsync(id);
			if (user == null) return NotFound();
			return Ok(user);
		}

		[HttpGet("exists/{username}")]
		public async Task<ActionResult<bool>> CheckExists(string username)
		{
			var exists = await userService.UserExistsAsync(username);
			return Ok(exists);
		}
	}
}
