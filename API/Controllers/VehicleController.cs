using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class VehicleController(IVehicleService _vehicleService) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Vehicle>>> GetAll()
		{
			var vehicles = await _vehicleService.GetAllAsync();
			return Ok(vehicles);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Vehicle>> GetById(int id)
		{
			var vehicle = await _vehicleService.GetByIdAsync(id);
			if (vehicle == null) return NotFound();
			return Ok(vehicle);
		}

		[HttpPost]
		public async Task<ActionResult<Vehicle>> Create([FromBody] Vehicle vehicle)
		{
			var created = await _vehicleService.CreateAsync(vehicle);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, [FromBody] Vehicle vehicle)
		{
			if (id != vehicle.Id) return BadRequest("ID mismatch");

			var result = await _vehicleService.UpdateAsync(vehicle);
			if (!result) return NotFound();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var result = await _vehicleService.DeleteAsync(id);
			if (!result) return NotFound();

			return NoContent();
		}

		[HttpGet("{vehicleId}/available-on/{date}")]
		public async Task<ActionResult<bool>> IsAvailable(int vehicleId, string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date format");

			var isAvailable = await _vehicleService.IsAvailableAsync(vehicleId, parsedDate);
			return Ok(isAvailable);
		}

		[HttpGet("{vehicleId}/remaining-capacity/{date}")]
		public async Task<ActionResult<double>> GetRemainingCapacity(int vehicleId, string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date format");

			var remaining = await _vehicleService.GetRemainingCapacityAsync(vehicleId, parsedDate);
			return Ok(remaining);
		}

		[HttpGet("available/{date}")]
		public async Task<ActionResult<IEnumerable<Vehicle>>> GetAvailableVehicles(string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date format");

			var vehicles = await _vehicleService.GetAvailableVehiclesAsync(parsedDate);
			return Ok(vehicles);
		}
	}
}
