using Application.DTOs;

namespace Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetDashboardSummaryAsync();
    Task<RevenueReportDto> GetRevenueReportAsync(DateTime startDate, DateTime endDate);
    Task<InventoryReportDto> GetInventoryReportAsync();
    Task<ExpenseReportDto> GetExpenseReportAsync(int month, int year);
    Task<DriverPerformanceDto> GetDriverPerformanceAsync(int month, int year);
    Task<ProfitLossDto> GetProfitLossAsync(int month, int year);
}
