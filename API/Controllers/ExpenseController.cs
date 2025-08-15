using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : BaseApiController
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> Create(CreateExpenseDto createDto)
    {
        var result = await _expenseService.CreateAsync(createDto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAll()
    {
        var result = await _expenseService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseDto>> GetById(int id)
    {
        var result = await _expenseService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetByCategory(string category)
    {
        var result = await _expenseService.GetByCategoryAsync(category);
        return Ok(result);
    }

    [HttpGet("date-range")]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetByDateRange(
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _expenseService.GetByDateRangeAsync(startDate, endDate);
        return Ok(result);
    }

    [HttpGet("vehicle/{vehicleId}")]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetByVehicle(int vehicleId)
    {
        var result = await _expenseService.GetByVehicleAsync(vehicleId);
        return Ok(result);
    }

    [HttpGet("driver/{driverId}")]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetByDriver(int driverId)
    {
        var result = await _expenseService.GetByDriverAsync(driverId);
        return Ok(result);
    }

    [HttpGet("monthly-total/{month}/{year}")]
    public async Task<ActionResult<double>> GetMonthlyTotal(int month, int year)
    {
        var result = await _expenseService.GetTotalExpensesByMonthAsync(month, year);
        return Ok(result);
    }

    [HttpGet("monthly-breakdown/{month}/{year}")]
    public async Task<ActionResult<Dictionary<string, double>>> GetMonthlyBreakdown(int month, int year)
    {
        var result = await _expenseService.GetExpenseBreakdownByMonthAsync(month, year);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ExpenseDto>> Update(int id, CreateExpenseDto updateDto)
    {
        var result = await _expenseService.UpdateAsync(id, updateDto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _expenseService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
