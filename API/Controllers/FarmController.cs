using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class FarmController(IFarmService farmService) : BaseApiController
	{
		[HttpGet("{id}")]
		public async Task<ActionResult<FarmDto>> GetFarmById(int id)
		{
			var farm = await farmService.GetFarmByIdAsync(id);
			if (farm == null) return NotFound();
			return Ok(farm);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Farm>>> GetAllFarms()
		{
			var farms = await farmService.GetAllFarmsAsync();
			return Ok(farms);
		}

		[HttpPost]
		public async Task<ActionResult> CreateFarm([FromBody] FarmDto farm)
		{
			if (farm == null) return BadRequest("Invalid farm data");
			await farmService.AddFarmAsync(farm);
			return CreatedAtAction(nameof(GetFarmById), new { id = farm.FramId }, farm);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateFarm(int id, [FromBody] FarmDto farm)
		{
			if (farm == null || id != farm.FramId)
				return BadRequest("Farm ID mismatch");

			var exists = await farmService.FarmExistsAsync(id);
			if (!exists) return NotFound();

			await farmService.UpdateFarmAsync(farm);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteFarm(int id)
		{
			var exists = await farmService.FarmExistsAsync(id);
			if (!exists) return NotFound();

			await farmService.DeleteFarmAsync(id);
			return NoContent();
		}

		[HttpGet("exists/{id}")]
		public async Task<ActionResult<bool>> CheckIfFarmExists(int id)
		{
			return Ok(await farmService.FarmExistsAsync(id));
		}

		// Optional: Add specification-based fetching
		//[HttpGet("with-spec")]
		//public async Task<ActionResult<IEnumerable<Farm>>> GetFarmsWithSpec()
		//{
		//	// Create and pass your specification here
		//	var spec = new AllFarmsWithStockSpecification(); // You define this spec class
		//	var farms = await farmService.GetFarmsWithSpecAsync(spec);
		//	return Ok(farms);
		//}

		//[HttpGet("count-with-spec")]
		//public async Task<ActionResult<int>> CountFarmsWithSpec()
		//{
		//	var spec = new AllFarmsWithStockSpecification(); // You define this spec class
		//	var count = await farmService.CountFarmsWithSpecAsync(spec);
		//	return Ok(count);
		//}
	}
}
