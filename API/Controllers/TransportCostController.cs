using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class TransportCostController(ITransportCostService _transportCostService) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<TransportCost>>> GetAll()
		{
			var costs = await _transportCostService.GetAllAsync();
			return Ok(costs);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TransportCost>> GetById(int id)
		{
			var cost = await _transportCostService.GetByIdAsync(id);
			if (cost == null) return NotFound();
			return Ok(cost);
		}

		[HttpPost]
		public async Task<ActionResult<TransportCost>> Create([FromBody] TransportCost cost)
		{
			var created = await _transportCostService.CreateAsync(cost);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] TransportCost cost)
		{
			if (id != cost.Id) return BadRequest("ID mismatch");

			var updated = await _transportCostService.UpdateAsync(cost);
			if (!updated) return NotFound();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _transportCostService.DeleteAsync(id);
			if (!deleted) return NotFound();
			return NoContent();
		}

		[HttpGet("date/{date}")]
		public async Task<ActionResult<IEnumerable<TransportCost>>> GetByDate(string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date format");

			var costs = await _transportCostService.GetByDateAsync(parsedDate);
			return Ok(costs);
		}

		[HttpGet("vehicle/{vehicleId}")]
		public async Task<ActionResult<IEnumerable<TransportCost>>> GetByVehicle(int vehicleId)
		{
			var costs = await _transportCostService.GetByVehicleAsync(vehicleId);
			return Ok(costs);
		}

		[HttpGet("total/date/{date}")]
		public async Task<ActionResult<decimal>> GetTotalCostByDate(string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date format");

			var total = await _transportCostService.GetTotalCostByDateAsync(parsedDate);
			return Ok(total);
		}

		[HttpGet("total/vehicle/{vehicleId}")]
		public async Task<ActionResult<decimal>> GetTotalCostByVehicle(int vehicleId)
		{
			var total = await _transportCostService.GetTotalCostByVehicleAsync(vehicleId);
			return Ok(total);
		}
	}
}
