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
		public async Task<ActionResult> Create([FromBody] CreateChickenBatchDto batch)
		{
			try
			{
				var chickenBatchDto = new ChickenBatchDto
				{
					CollectionDate = batch.CollectionDate,
					NumberOfChickens = batch.NumberOfChickens,
					QuantityInKg = batch.QuantityInKg,
					Type = batch.Type,
					Notes = batch.Notes,
					FarmerId = batch.FarmerId,
					FarmerName = batch.FarmerName,
					CollectionWeight = batch.CollectionWeight
				};
				
				await CBService.AddAsync(chickenBatchDto);
				return CreatedAtAction(nameof(GetById), new { id = chickenBatchDto.Id }, chickenBatchDto);
			}
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, [FromBody] CreateChickenBatchDto batch)
		{
			try
			{
				var chickenBatchDto = new ChickenBatchDto
				{
					CollectionDate = batch.CollectionDate,
					NumberOfChickens = batch.NumberOfChickens,
					QuantityInKg = batch.QuantityInKg,
					Type = batch.Type,
					Notes = batch.Notes,
					FarmerId = batch.FarmerId,
					FarmerName = batch.FarmerName,
					CollectionWeight = batch.CollectionWeight
				};
				
				await CBService.UpdateAsync(id, chickenBatchDto);
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
	}
}
