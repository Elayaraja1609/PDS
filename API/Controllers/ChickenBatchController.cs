using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class ChickenBatchController(IChickenBatchService CBService) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ChickenBatchDto>>> GetAll()
		{
			var batches = await CBService.GetAllAsync();
			return Ok(batches);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ChickenBatchDto>> GetById(int id)
		{
			var batch = await CBService.GetByIdAsync(id);
			if (batch == null) return NotFound();
			return Ok(batch);
		}

		[HttpPost]
		public async Task<ActionResult> Create([FromBody] ChickenBatchDto batch)
		{
			try
			{
				await CBService.AddAsync(batch);
				return CreatedAtAction(nameof(GetById), new { id = batch.Id }, batch);
			}
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, [FromBody] ChickenBatchDto batch)
		{
			if (id != batch.Id) return BadRequest("ID mismatch");
			try
			{
				await CBService.UpdateAsync(batch);
				return Ok(new
				{
					StatusCode = StatusCodes.Status200OK,
					Message = "Chicken batch updated successfully."
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				await CBService.DeleteAsync(id);
				return Ok(new
				{
					StatusCode = StatusCodes.Status200OK,
					Message = "Deleted successfully."
				});
			}
			catch (Exception ex)
			{
				return NotFound(new { error = ex.Message });
			}
		}

		[HttpGet("farmer/{farmerId}")]
		public async Task<ActionResult<IEnumerable<ChickenBatchDto>>> GetByFarmer(int farmerId)
		{
			var result = await CBService.GetByFarmerIdAsync(farmerId);
			return Ok(result);
		}

		[HttpGet("date/{date}")]
		public async Task<ActionResult<IEnumerable<ChickenBatchDto>>> GetByDate(string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date format");

			var result = await CBService.GetByDateAsync(parsedDate);
			return Ok(result);
		}
	}
}
