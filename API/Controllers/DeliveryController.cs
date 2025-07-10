using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class DeliveryController(IDeliveryService _deliveryService) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<DeliveryDto>>> GetAll()
		{
			var deliveries = await _deliveryService.GetAllAsync();
			return Ok(deliveries);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DeliveryDto>> GetById(int id)
		{
			var delivery = await _deliveryService.GetByIdAsync(id);
			if (delivery == null) return NotFound();
			return Ok(delivery);
		}

		[HttpPost]
		public async Task<ActionResult<DeliveryDto>> Create([FromBody] DeliveryDto delivery)
		{
			var created = await _deliveryService.CreateAsync(delivery);
			return CreatedAtAction(nameof(GetById), new { id = created.DeliveryId }, created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] DeliveryDto delivery)
		{
			if (id != delivery.DeliveryId) return BadRequest("ID mismatch");

			var success = await _deliveryService.UpdateAsync(delivery);
			if (!success) return NotFound();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var success = await _deliveryService.DeleteAsync(id);
			if (!success) return NotFound();

			return NoContent();
		}

		[HttpGet("date/{date}")]
		public async Task<ActionResult<IEnumerable<DeliveryDto>>> GetByDate(string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date");

			var deliveries = await _deliveryService.GetDeliveriesByDateAsync(parsedDate);
			return Ok(deliveries);
		}

		[HttpGet("driver/{driverId}")]
		public async Task<ActionResult<IEnumerable<DeliveryDto>>> GetByDriver(int driverId, [FromQuery] string? date)
		{
			DateTime? parsedDate = null;
			if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date, out var dt))
				parsedDate = dt;

			var deliveries = await _deliveryService.GetDeliveriesByDriverAsync(driverId, parsedDate);
			return Ok(deliveries);
		}

		[HttpGet("vehicle/{vehicleId}")]
		public async Task<ActionResult<IEnumerable<DeliveryDto>>> GetByVehicle(int vehicleId, [FromQuery] string? date)
		{
			DateTime? parsedDate = null;
			if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date, out var dt))
				parsedDate = dt;

			var deliveries = await _deliveryService.GetDeliveriesByVehicleAsync(vehicleId, parsedDate);
			return Ok(deliveries);
		}

		[HttpGet("{deliveryId}/remaining-load")]
		public async Task<ActionResult<double>> GetRemainingLoad(int deliveryId)
		{
			var remaining = await _deliveryService.GetRemainingLoadInVehicleAsync(deliveryId);
			return Ok(remaining);
		}

		[HttpPost("{deliveryId}/assign-order")]
		public async Task<ActionResult> AssignOrder(int deliveryId, [FromBody] Order order)
		{
			var success = await _deliveryService.AssignOrderToDeliveryAsync(deliveryId, order);
			if (!success) return BadRequest("Unable to assign order to delivery. Check capacity or delivery ID.");
			return Ok();
		}
	}
}
