using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : BaseApiController
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("summary")]
    public async Task<ActionResult<DashboardSummaryDto>> GetSummary()
    {
        var result = await _dashboardService.GetDashboardSummaryAsync();
        return Ok(result);
    }

    [HttpGet("revenue")]
    public async Task<ActionResult<RevenueReportDto>> GetRevenueReport(
        [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _dashboardService.GetRevenueReportAsync(startDate, endDate);
        return Ok(result);
    }

    [HttpGet("inventory")]
    public async Task<ActionResult<InventoryReportDto>> GetInventoryReport()
    {
        var result = await _dashboardService.GetInventoryReportAsync();
        return Ok(result);
    }

    [HttpGet("expenses/{month}/{year}")]
    public async Task<ActionResult<ExpenseReportDto>> GetExpenseReport(int month, int year)
    {
        var result = await _dashboardService.GetExpenseReportAsync(month, year);
        return Ok(result);
    }

    [HttpGet("driver-performance/{month}/{year}")]
    public async Task<ActionResult<DriverPerformanceDto>> GetDriverPerformance(int month, int year)
    {
        var result = await _dashboardService.GetDriverPerformanceAsync(month, year);
        return Ok(result);
    }

    [HttpGet("profit-loss/{month}/{year}")]
    public async Task<ActionResult<ProfitLossDto>> GetProfitLoss(int month, int year)
    {
        var result = await _dashboardService.GetProfitLossAsync(month, year);
        return Ok(result);
    }
}
