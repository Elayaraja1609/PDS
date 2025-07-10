using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class PaymentController(IPaymentService _paymentService) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAll()
		{
			var payments = await _paymentService.GetAllAsync();
			return Ok(payments);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<PaymentDto>> GetById(int id)
		{
			var payment = await _paymentService.GetByIdAsync(id);
			if (payment == null) return NotFound();
			return Ok(payment);
		}

		[HttpPost]
		public async Task<ActionResult<PaymentDto>> Create([FromBody] PaymentDto payment)
		{
			try
			{
				var created = await _paymentService.CreateAsync(payment);
				return CreatedAtAction(nameof(GetById), new { id = created.PaymentId }, created);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _paymentService.DeleteAsync(id);
			if (!deleted) return NotFound("Payment not found");

			return NoContent();
		}

		[HttpGet("shop/{shopId}")]
		public async Task<ActionResult<IEnumerable<PaymentDto>>> GetByShop(int shopId)
		{
			var payments = await _paymentService.GetPaymentsByShopAsync(shopId);
			return Ok(payments);
		}

		[HttpGet("order/{orderId}")]
		public async Task<ActionResult<IEnumerable<PaymentDto>>> GetByOrder(int orderId)
		{
			var payments = await _paymentService.GetPaymentsByOrderAsync(orderId);
			return Ok(payments);
		}

		[HttpGet("shop/{shopId}/total")]
		public async Task<ActionResult<decimal>> GetTotalPaidByShop(int shopId)
		{
			var total = await _paymentService.GetTotalPaidByShopAsync(shopId);
			return Ok(total);
		}
	}
}
