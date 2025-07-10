using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class FarmStockController(IFarmStockService _farmStockService) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<FarmStockDto>>> GetAll()
		{
			var stocks = await _farmStockService.GetAllAsync();
			return Ok(stocks);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<FarmStockDto>> GetById(int id)
		{
			var stock = await _farmStockService.GetByIdAsync(id);
			if (stock == null) return NotFound();
			return Ok(stock);
		}

		[HttpPost]
		public async Task<ActionResult<FarmStockDto>> Create([FromBody] FarmStockDto stock)
		{
			var created = await _farmStockService.CreateAsync(stock);
			return CreatedAtAction(nameof(GetById), new { id = created.FarmStockId }, created);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, [FromBody] FarmStockDto stock)
		{
			if (id != stock.FarmStockId) return BadRequest("ID mismatch");

			var result = await _farmStockService.UpdateAsync(stock);
			if (!result) return NotFound();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var result = await _farmStockService.DeleteAsync(id);
			if (!result) return NotFound();

			return NoContent();
		}

		[HttpGet("latest")]
		public async Task<ActionResult<FarmStockDto>> GetLatestStock()
		{
			var stock = await _farmStockService.GetLatestStockAsync();
			if (stock == null) return NotFound();
			return Ok(stock);
		}

		[HttpGet("available")]
		public async Task<ActionResult<double>> GetAvailableStock()
		{
			var available = await _farmStockService.GetAvailableStockAsync();
			return Ok(available);
		}

		[HttpPost("adjust")]
		public async Task<ActionResult> AdjustStock([FromBody] StockAdjustmentDto dto)
		{
			var result = await _farmStockService.AdjustStockAsync(dto.Quantity, dto.Increase);
			if (!result) return BadRequest("Unable to adjust stock");
			return Ok();
		}
	}
}
