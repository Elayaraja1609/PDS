using Application.DTOs;

namespace Application.Interfaces;

public interface ISalaryService
{
    Task<SalaryDto> CreateAsync(CreateSalaryDto createDto);
    Task<SalaryDto> GetByIdAsync(int id);
    Task<IEnumerable<SalaryDto>> GetAllAsync();
    Task<IEnumerable<SalaryDto>> GetByDriverAsync(int driverId);
    Task<IEnumerable<SalaryDto>> GetByMonthAsync(int month, int year);
    Task<SalaryDto> UpdateAsync(int id, CreateSalaryDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> MarkAsPaidAsync(int id, string paymentMethod);
    Task<PayrollSummaryDto> GetPayrollSummaryAsync(int month, int year);
    Task<bool> GenerateMonthlySalariesAsync(int month, int year);
    Task<double> CalculateSalaryAsync(int driverId, int month, int year);
}
