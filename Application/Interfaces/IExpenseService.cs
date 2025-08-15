using Application.DTOs;

namespace Application.Interfaces;

public interface IExpenseService
{
    Task<ExpenseDto> CreateAsync(CreateExpenseDto createDto);
    Task<ExpenseDto> GetByIdAsync(int id);
    Task<IEnumerable<ExpenseDto>> GetAllAsync();
    Task<IEnumerable<ExpenseDto>> GetByCategoryAsync(string category);
    Task<IEnumerable<ExpenseDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<ExpenseDto>> GetByVehicleAsync(int vehicleId);
    Task<IEnumerable<ExpenseDto>> GetByDriverAsync(int driverId);
    Task<ExpenseDto> UpdateAsync(int id, CreateExpenseDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<double> GetTotalExpensesByMonthAsync(int month, int year);
    Task<Dictionary<string, double>> GetExpenseBreakdownByMonthAsync(int month, int year);
}
