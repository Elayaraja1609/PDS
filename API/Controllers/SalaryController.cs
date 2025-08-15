using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalaryController : BaseApiController
{
    private readonly ISalaryService _salaryService;

    public SalaryController(ISalaryService salaryService)
    {
        _salaryService = salaryService;
    }

    [HttpPost]
    public async Task<ActionResult<SalaryDto>> Create(CreateSalaryDto createDto)
    {
        var result = await _salaryService.CreateAsync(createDto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalaryDto>>> GetAll()
    {
        var result = await _salaryService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SalaryDto>> GetById(int id)
    {
        var result = await _salaryService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("driver/{driverId}")]
    public async Task<ActionResult<IEnumerable<SalaryDto>>> GetByDriver(int driverId)
    {
        var result = await _salaryService.GetByDriverAsync(driverId);
        return Ok(result);
    }

    [HttpGet("month/{month}/{year}")]
    public async Task<ActionResult<IEnumerable<SalaryDto>>> GetByMonth(int month, int year)
    {
        var result = await _salaryService.GetByMonthAsync(month, year);
        return Ok(result);
    }

    [HttpGet("payroll-summary/{month}/{year}")]
    public async Task<ActionResult<PayrollSummaryDto>> GetPayrollSummary(int month, int year)
    {
        var result = await _salaryService.GetPayrollSummaryAsync(month, year);
        return Ok(result);
    }

    [HttpPost("generate-monthly/{month}/{year}")]
    public async Task<ActionResult<bool>> GenerateMonthlySalaries(int month, int year)
    {
        var result = await _salaryService.GenerateMonthlySalariesAsync(month, year);
        return Ok(result);
    }

    [HttpPost("calculate/{driverId}/{month}/{year}")]
    public async Task<ActionResult<double>> CalculateSalary(int driverId, int month, int year)
    {
        var result = await _salaryService.CalculateSalaryAsync(driverId, month, year);
        return Ok(result);
    }

    [HttpPost("mark-paid/{id}")]
    public async Task<ActionResult<bool>> MarkAsPaid(int id, [FromBody] string paymentMethod)
    {
        var result = await _salaryService.MarkAsPaidAsync(id, paymentMethod);
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SalaryDto>> Update(int id, CreateSalaryDto updateDto)
    {
        var result = await _salaryService.UpdateAsync(id, updateDto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _salaryService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
