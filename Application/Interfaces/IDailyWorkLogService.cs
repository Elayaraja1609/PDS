using Application.DTOs;

namespace Application.Interfaces;

public interface IDailyWorkLogService
{
    Task<DailyWorkLogDto> CreateAsync(CreateDailyWorkLogDto createDto);
    Task<DailyWorkLogDto> GetByIdAsync(int id);
    Task<IEnumerable<DailyWorkLogDto>> GetAllAsync();
    Task<IEnumerable<DailyWorkLogDto>> GetByDriverAsync(int driverId);
    Task<IEnumerable<DailyWorkLogDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<DailyWorkLogDto> UpdateAsync(int id, CreateDailyWorkLogDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<DailyWorkLogDto>> GetMonthlyWorkLogAsync(int driverId, int month, int year);
}
