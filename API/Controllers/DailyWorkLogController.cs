using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DailyWorkLogController : BaseApiController
{
    private readonly IDailyWorkLogService _workLogService;

    public DailyWorkLogController(IDailyWorkLogService workLogService)
    {
        _workLogService = workLogService;
    }

    [HttpPost]
    public async Task<ActionResult<DailyWorkLogDto>> Create(CreateDailyWorkLogDto createDto)
    {
        var result = await _workLogService.CreateAsync(createDto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DailyWorkLogDto>>> GetAll()
    {
        var result = await _workLogService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DailyWorkLogDto>> GetById(int id)
    {
        var result = await _workLogService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("driver/{driverId}")]
    public async Task<ActionResult<IEnumerable<DailyWorkLogDto>>> GetByDriver(int driverId)
    {
        var result = await _workLogService.GetByDriverAsync(driverId);
        return Ok(result);
    }

    [HttpGet("date-range")]
    public async Task<ActionResult<IEnumerable<DailyWorkLogDto>>> GetByDateRange(
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _workLogService.GetByDateRangeAsync(startDate, endDate);
        return Ok(result);
    }

    [HttpGet("monthly/{driverId}/{month}/{year}")]
    public async Task<ActionResult<IEnumerable<DailyWorkLogDto>>> GetMonthlyWorkLog(
        int driverId, int month, int year)
    {
        var result = await _workLogService.GetMonthlyWorkLogAsync(driverId, month, year);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DailyWorkLogDto>> Update(int id, CreateDailyWorkLogDto updateDto)
    {
        var result = await _workLogService.UpdateAsync(id, updateDto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _workLogService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
