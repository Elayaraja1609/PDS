using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IInventoryService _inventoryService;
    private readonly IGenericRepo<Delivery> _deliveryRepo;
    private readonly IGenericRepo<Driver> _driverRepo;
    private readonly IGenericRepo<Expense> _expenseRepo;
    private readonly IGenericRepo<DailyWorkLog> _workLogRepo;

    public DashboardService(
        IInventoryService inventoryService,
        IGenericRepo<Delivery> deliveryRepo,
        IGenericRepo<Driver> driverRepo,
        IGenericRepo<Expense> expenseRepo,
        IGenericRepo<DailyWorkLog> workLogRepo)
    {
        _inventoryService = inventoryService;
        _deliveryRepo = deliveryRepo;
        _driverRepo = driverRepo;
        _expenseRepo = expenseRepo;
        _workLogRepo = workLogRepo;
    }

    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
    {
        var currentInventory = await _inventoryService.GetCurrentInventoryAsync();
        var today = DateTime.Today;
        var monthStart = new DateTime(today.Year, today.Month, 1);
        var monthEnd = monthStart.AddMonths(1).AddDays(-1);

        var todayRevenue = await GetDailyRevenueAsync(today);
        var monthlyRevenue = await GetMonthlyRevenueAsync(monthStart, monthEnd);
        var activeDeliveries = await GetActiveDeliveriesCountAsync();
        var activeDrivers = await GetActiveDriversCountAsync();
        var monthlyExpenses = await GetMonthlyExpensesAsync(today.Month, today.Year);
        var monthlyProfit = monthlyRevenue - monthlyExpenses;

        return new DashboardSummaryDto
        {
            CurrentInventory = currentInventory,
            TodayRevenue = todayRevenue,
            MonthlyRevenue = monthlyRevenue,
            ActiveDeliveries = activeDeliveries,
            ActiveDrivers = activeDrivers,
            MonthlyExpenses = monthlyExpenses,
            MonthlyProfit = monthlyProfit
        };
    }

    public async Task<RevenueReportDto> GetRevenueReportAsync(DateTime startDate, DateTime endDate)
    {
        var deliveries = await _deliveryRepo.GetAsync(x => 
            x.DeliveryDate >= startDate && 
            x.DeliveryDate <= endDate &&
            x.Status == "Completed");

        var totalRevenue = deliveries.Sum(x => x.TotalAmount);
        var totalDeliveries = deliveries.Count();
        var averageDeliveryValue = totalDeliveries > 0 ? totalRevenue / totalDeliveries : 0;

        var dailyRevenue = deliveries
            .GroupBy(x => x.DeliveryDate.Date)
            .Select(g => new DailyRevenueDto
            {
                Date = g.Key,
                Revenue = g.Sum(x => x.TotalAmount),
                Deliveries = g.Count()
            })
            .OrderBy(x => x.Date)
            .ToList();

        return new RevenueReportDto
        {
            StartDate = startDate,
            EndDate = endDate,
            TotalRevenue = totalRevenue,
            TotalDeliveries = totalDeliveries,
            AverageDeliveryValue = averageDeliveryValue,
            DailyRevenue = dailyRevenue
        };
    }

    public async Task<InventoryReportDto> GetInventoryReportAsync()
    {
        var currentInventory = await _inventoryService.GetCurrentInventoryAsync();
        var inventoryBreakdown = await _inventoryService.GetInventoryBreakdownAsync();
        var expiringBatches = await _inventoryService.GetExpiringBatchesAsync();
        var inventoryValue = await _inventoryService.GetInventoryValueAsync();

        return new InventoryReportDto
        {
            TotalInventory = currentInventory,
            InventoryByType = inventoryBreakdown,
            ExpiringBatches = expiringBatches.ToList(),
            InventoryValue = inventoryValue
        };
    }

    public async Task<ExpenseReportDto> GetExpenseReportAsync(int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var expenses = await _expenseRepo.GetAsync(x => 
            x.ExpenseDate >= startDate && 
            x.ExpenseDate <= endDate);

        var totalExpenses = expenses.Sum(x => x.Amount);
        var expensesByCategory = expenses
            .GroupBy(x => x.Category)
            .ToDictionary(g => g.Key, g => g.Sum(x => x.Amount));

        var fuelExpenses = expenses.Where(x => x.Category == "Fuel").Sum(x => x.Amount);
        var maintenanceExpenses = expenses.Where(x => x.Category == "Maintenance").Sum(x => x.Amount);
        var salaryExpenses = expenses.Where(x => x.Category == "Salary").Sum(x => x.Amount);
        var otherExpenses = totalExpenses - fuelExpenses - maintenanceExpenses - salaryExpenses;

        return new ExpenseReportDto
        {
            Month = month,
            Year = year,
            TotalExpenses = totalExpenses,
            ExpensesByCategory = expensesByCategory,
            FuelExpenses = fuelExpenses,
            MaintenanceExpenses = maintenanceExpenses,
            SalaryExpenses = salaryExpenses,
            OtherExpenses = otherExpenses
        };
    }

    public async Task<DriverPerformanceDto> GetDriverPerformanceAsync(int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var drivers = await _driverRepo.GetAsync(x => x.IsActive);
        var driverPerformance = new List<DriverPerformanceDetailDto>();

        foreach (var driver in drivers)
        {
            var workLogs = await _workLogRepo.GetAsync(x => 
                x.DriverId == driver.Id && 
                x.WorkDate >= startDate && 
                x.WorkDate <= endDate &&
                x.IsPresent);

            var deliveries = await _deliveryRepo.GetAsync(x => 
                x.DriverId == driver.Id && 
                x.DeliveryDate >= startDate && 
                x.DeliveryDate <= endDate &&
                x.Status == "Completed");

            var daysWorked = workLogs.Count();
            var deliveriesCompleted = deliveries.Count();
            var totalDistance = 0.0; // This would need to be calculated from actual route data
            var fuelEfficiency = 0.0; // This would need to be calculated from fuel consumption data
            var performanceScore = CalculatePerformanceScore(daysWorked, deliveriesCompleted);

            driverPerformance.Add(new DriverPerformanceDetailDto
            {
                DriverId = driver.Id,
                DriverName = driver.Name,
                DaysWorked = daysWorked,
                DeliveriesCompleted = deliveriesCompleted,
                TotalDistance = totalDistance,
                FuelEfficiency = fuelEfficiency,
                PerformanceScore = performanceScore
            });
        }

        return new DriverPerformanceDto
        {
            Month = month,
            Year = year,
            Drivers = driverPerformance.OrderByDescending(x => x.PerformanceScore).ToList()
        };
    }

    public async Task<ProfitLossDto> GetProfitLossAsync(int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var revenueReport = await GetRevenueReportAsync(startDate, endDate);
        var expenseReport = await GetExpenseReportAsync(month, year);

        var totalRevenue = revenueReport.TotalRevenue;
        var totalExpenses = expenseReport.TotalExpenses;
        var grossProfit = totalRevenue - expenseReport.FuelExpenses - expenseReport.MaintenanceExpenses;
        var netProfit = grossProfit - expenseReport.SalaryExpenses - expenseReport.OtherExpenses;
        var profitMargin = totalRevenue > 0 ? (netProfit / totalRevenue) * 100 : 0;

        var breakdown = new List<ProfitLossBreakdownDto>
        {
            new ProfitLossBreakdownDto
            {
                Category = "Chicken Sales",
                Revenue = totalRevenue,
                Expenses = 0,
                Profit = totalRevenue
            },
            new ProfitLossBreakdownDto
            {
                Category = "Fuel & Transport",
                Revenue = 0,
                Expenses = expenseReport.FuelExpenses + expenseReport.MaintenanceExpenses,
                Profit = -(expenseReport.FuelExpenses + expenseReport.MaintenanceExpenses)
            },
            new ProfitLossBreakdownDto
            {
                Category = "Staff Salaries",
                Revenue = 0,
                Expenses = expenseReport.SalaryExpenses,
                Profit = -expenseReport.SalaryExpenses
            },
            new ProfitLossBreakdownDto
            {
                Category = "Other Expenses",
                Revenue = 0,
                Expenses = expenseReport.OtherExpenses,
                Profit = -expenseReport.OtherExpenses
            }
        };

        return new ProfitLossDto
        {
            Month = month,
            Year = year,
            TotalRevenue = totalRevenue,
            TotalExpenses = totalExpenses,
            GrossProfit = grossProfit,
            NetProfit = netProfit,
            ProfitMargin = profitMargin,
            Breakdown = breakdown
        };
    }

    private async Task<double> GetDailyRevenueAsync(DateTime date)
    {
        var deliveries = await _deliveryRepo.GetAsync(x => 
            x.DeliveryDate.Date == date.Date && 
            x.Status == "Completed");
        return deliveries.Sum(x => x.TotalAmount);
    }

    private async Task<double> GetMonthlyRevenueAsync(DateTime startDate, DateTime endDate)
    {
        var deliveries = await _deliveryRepo.GetAsync(x => 
            x.DeliveryDate >= startDate && 
            x.DeliveryDate <= endDate &&
            x.Status == "Completed");
        return deliveries.Sum(x => x.TotalAmount);
    }

    private async Task<int> GetActiveDeliveriesCountAsync()
    {
        var activeDeliveries = await _deliveryRepo.GetAsync(x => 
            x.Status == "Pending" || x.Status == "InProgress");
        return activeDeliveries.Count();
    }

    private async Task<int> GetActiveDriversCountAsync()
    {
        var activeDrivers = await _driverRepo.GetAsync(x => x.IsActive);
        return activeDrivers.Count();
    }

    private async Task<double> GetMonthlyExpensesAsync(int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        var expenses = await _expenseRepo.GetAsync(x => 
            x.ExpenseDate >= startDate && 
            x.ExpenseDate <= endDate);
        return expenses.Sum(x => x.Amount);
    }

    private double CalculatePerformanceScore(int daysWorked, int deliveriesCompleted)
    {
        // Simple performance scoring algorithm
        var attendanceScore = Math.Min(daysWorked / 22.0, 1.0) * 50; // Max 50 points for attendance
        var deliveryScore = Math.Min(deliveriesCompleted / 30.0, 1.0) * 50; // Max 50 points for deliveries
        return attendanceScore + deliveryScore;
    }
}
