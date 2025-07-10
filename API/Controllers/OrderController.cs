using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class OrderController(IOrderService _orderService) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
		{
			var orders = await _orderService.GetAllAsync();
			return Ok(orders);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderDto>> GetById(int id)
		{
			var order = await _orderService.GetByIdAsync(id);
			if (order == null) return NotFound();
			return Ok(order);
		}

		[HttpPost]
		public async Task<ActionResult<OrderDto>> Create([FromBody] OrderDto order)
		{
			var created = await _orderService.CreateAsync(order);
			return CreatedAtAction(nameof(GetById), new { id = created.OrderId }, created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] OrderDto order)
		{
			if (id != order.OrderId) return BadRequest("ID mismatch");

			var result = await _orderService.UpdateAsync(order);
			if (!result) return NotFound();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _orderService.DeleteAsync(id);
			if (!result) return NotFound();

			return NoContent();
		}

		[HttpGet("shop/{shopId}")]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetByShop(int shopId)
		{
			var orders = await _orderService.GetOrdersByShopAsync(shopId);
			return Ok(orders);
		}

		[HttpGet("date/{date}")]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetByDate(string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date");

			var orders = await _orderService.GetOrdersByDateAsync(parsedDate);
			return Ok(orders);
		}

		[HttpGet("delivery/{deliveryId}")]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetByDelivery(int deliveryId)
		{
			var orders = await _orderService.GetOrdersByDeliveryAsync(deliveryId);
			return Ok(orders);
		}

		[HttpGet("{orderId}/total")]
		public async Task<ActionResult<decimal>> GetTotalAmount(int orderId)
		{
			var total = await _orderService.GetTotalAmountAsync(orderId);
			return Ok(total);
		}

		[HttpGet("{orderId}/balance")]
		public async Task<ActionResult<decimal>> GetBalanceAmount(int orderId)
		{
			var balance = await _orderService.GetBalanceAmountAsync(orderId);
			return Ok(balance);
		}

		[HttpPost("{orderId}/record-payment")]
		public async Task<IActionResult> RecordPayment(int orderId, [FromBody] decimal amount)
		{
			var success = await _orderService.RecordPaymentAsync(orderId, amount);
			if (!success) return NotFound("Order not found or payment failed.");

			return Ok("Payment recorded successfully.");
		}
	}
}
