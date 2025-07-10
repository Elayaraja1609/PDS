using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class DriverController(IDriverService _driverService) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Driver>>> GetAll()
		{
			var drivers = await _driverService.GetAllAsync();
			return Ok(drivers);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Driver>> GetById(int id)
		{
			var driver = await _driverService.GetByIdAsync(id);
			if (driver == null) return NotFound();
			return Ok(driver);
		}

		[HttpPost]
		public async Task<ActionResult<Driver>> Create([FromBody] Driver driver)
		{
			var created = await _driverService.CreateAsync(driver);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, [FromBody] Driver driver)
		{
			if (id != driver.Id) return BadRequest("ID mismatch");

			var result = await _driverService.UpdateAsync(driver);
			if (!result) return NotFound();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var result = await _driverService.DeleteAsync(id);
			if (!result) return NotFound();

			return NoContent();
		}

		[HttpGet("{driverId}/available-on/{date}")]
		public async Task<ActionResult<bool>> IsAvailable(int driverId, string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date format");

			var available = await _driverService.IsAvailableAsync(driverId, parsedDate);
			return Ok(available);
		}

		[HttpGet("available/{date}")]
		public async Task<ActionResult<IEnumerable<Driver>>> GetAvailableDrivers(string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date format");

			var availableDrivers = await _driverService.GetAvailableDriversAsync(parsedDate);
			return Ok(availableDrivers);
		}

		[HttpGet("{driverId}/deliveries")]
		public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries(int driverId, [FromQuery] string? date)
		{
			DateTime? parsedDate = null;

			if (!string.IsNullOrWhiteSpace(date))
			{
				if (!DateTime.TryParse(date, out var dt))
					return BadRequest("Invalid date format");
				parsedDate = dt;
			}

			var deliveries = await _driverService.GetDeliveriesByDriverAsync(driverId, parsedDate);
			return Ok(deliveries);
		}
	}
}
