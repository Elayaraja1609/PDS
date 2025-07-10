using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class MiscExpenseController(IMiscExpenseService _miscExpenseService) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MiscExpense>>> GetAll()
		{
			var expenses = await _miscExpenseService.GetAllAsync();
			return Ok(expenses);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<MiscExpense>> GetById(int id)
		{
			var expense = await _miscExpenseService.GetByIdAsync(id);
			if (expense == null) return NotFound();
			return Ok(expense);
		}

		[HttpPost]
		public async Task<ActionResult<MiscExpense>> Create([FromBody] MiscExpense expense)
		{
			var created = await _miscExpenseService.CreateAsync(expense);
			return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] MiscExpense expense)
		{
			if (id != expense.Id) return BadRequest("ID mismatch");

			var updated = await _miscExpenseService.UpdateAsync(expense);
			if (!updated) return NotFound();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _miscExpenseService.DeleteAsync(id);
			if (!deleted) return NotFound();
			return NoContent();
		}

		[HttpGet("date/{date}")]
		public async Task<ActionResult<IEnumerable<MiscExpense>>> GetByDate(string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date format");

			var expenses = await _miscExpenseService.GetByDateAsync(parsedDate);
			return Ok(expenses);
		}

		[HttpGet("total/date/{date}")]
		public async Task<ActionResult<decimal>> GetTotalExpensesByDate(string date)
		{
			if (!DateTime.TryParse(date, out var parsedDate))
				return BadRequest("Invalid date format");

			var total = await _miscExpenseService.GetTotalExpensesByDateAsync(parsedDate);
			return Ok(total);
		}
	}
}
