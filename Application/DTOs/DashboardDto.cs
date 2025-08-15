namespace Application.DTOs;

public class DashboardSummaryDto
{
    public double CurrentInventory { get; set; }
    public double TodayRevenue { get; set; }
    public double MonthlyRevenue { get; set; }
    public int ActiveDeliveries { get; set; }
    public int ActiveDrivers { get; set; }
    public double MonthlyExpenses { get; set; }
    public double MonthlyProfit { get; set; }
}

public class RevenueReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalRevenue { get; set; }
    public int TotalDeliveries { get; set; }
    public double AverageDeliveryValue { get; set; }
    public List<DailyRevenueDto> DailyRevenue { get; set; } = new List<DailyRevenueDto>();
}

public class DailyRevenueDto
{
    public DateTime Date { get; set; }
    public double Revenue { get; set; }
    public int Deliveries { get; set; }
}

public class InventoryReportDto
{
    public double TotalInventory { get; set; }
    public Dictionary<string, double> InventoryByType { get; set; } = new Dictionary<string, double>();
    public List<ChickenBatchDto> ExpiringBatches { get; set; } = new List<ChickenBatchDto>();
    public double InventoryValue { get; set; }
}

public class ExpenseReportDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public double TotalExpenses { get; set; }
    public Dictionary<string, double> ExpensesByCategory { get; set; } = new Dictionary<string, double>();
    public double FuelExpenses { get; set; }
    public double MaintenanceExpenses { get; set; }
    public double SalaryExpenses { get; set; }
    public double OtherExpenses { get; set; }
}

public class DriverPerformanceDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public List<DriverPerformanceDetailDto> Drivers { get; set; } = new List<DriverPerformanceDetailDto>();
}

public class DriverPerformanceDetailDto
{
    public int DriverId { get; set; }
    public string DriverName { get; set; }
    public int DaysWorked { get; set; }
    public int DeliveriesCompleted { get; set; }
    public double TotalDistance { get; set; }
    public double FuelEfficiency { get; set; }
    public double PerformanceScore { get; set; }
}

public class ProfitLossDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public double TotalRevenue { get; set; }
    public double TotalExpenses { get; set; }
    public double GrossProfit { get; set; }
    public double NetProfit { get; set; }
    public double ProfitMargin { get; set; }
    public List<ProfitLossBreakdownDto> Breakdown { get; set; } = new List<ProfitLossBreakdownDto>();
}

public class ProfitLossBreakdownDto
{
    public string Category { get; set; }
    public double Revenue { get; set; }
    public double Expenses { get; set; }
    public double Profit { get; set; }
}
