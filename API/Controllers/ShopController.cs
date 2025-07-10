using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class ShopController(IShopService _shopService) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Shop>>> GetAll()
		{
			var shops = await _shopService.GetAllAsync();
			return Ok(shops);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Shop>> GetById(int id)
		{
			var shop = await _shopService.GetByIdAsync(id);
			if (shop == null) return NotFound();
			return Ok(shop);
		}

		[HttpPost]
		public async Task<ActionResult<Shop>> Create([FromBody] Shop shop)
		{
			var created = await _shopService.CreateAsync(shop);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] Shop shop)
		{
			if (id != shop.Id) return BadRequest("ID mismatch");

			var updated = await _shopService.UpdateAsync(shop);
			if (!updated) return NotFound();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _shopService.DeleteAsync(id);
			if (!deleted) return NotFound();

			return NoContent();
		}

		[HttpGet("{shopId}/orders")]
		public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int shopId, [FromQuery] string? date)
		{
			DateTime? parsedDate = null;
			if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date, out var dt))
				parsedDate = dt;

			var orders = await _shopService.GetOrdersByShopAsync(shopId, parsedDate);
			return Ok(orders);
		}

		[HttpGet("{shopId}/totals/due")]
		public async Task<ActionResult<decimal>> GetTotalDue(int shopId)
		{
			var totalDue = await _shopService.GetTotalDueAmountAsync(shopId);
			return Ok(totalDue);
		}

		[HttpGet("{shopId}/totals/paid")]
		public async Task<ActionResult<decimal>> GetTotalPaid(int shopId)
		{
			var totalPaid = await _shopService.GetTotalPaidAmountAsync(shopId);
			return Ok(totalPaid);
		}

		[HttpGet("{shopId}/totals/balance")]
		public async Task<ActionResult<decimal>> GetBalance(int shopId)
		{
			var balance = await _shopService.GetBalanceAmountAsync(shopId);
			return Ok(balance);
		}
	}
}
