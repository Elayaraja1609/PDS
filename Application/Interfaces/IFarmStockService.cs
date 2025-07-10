using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;
public interface IFarmStockService
{
	Task<IEnumerable<FarmStockDto>> GetAllAsync();
	Task<FarmStockDto?> GetByIdAsync(int id);
	Task<FarmStockDto> CreateAsync(FarmStockDto stock);
	Task<bool> UpdateAsync(FarmStockDto stock);
	Task<bool> DeleteAsync(int id);

	Task<double> GetAvailableStockAsync();
	Task<bool> AdjustStockAsync(double quantity, bool increase); // true: add, false: subtract
	Task<FarmStockDto?> GetLatestStockAsync();
}
