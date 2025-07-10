using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IChickenBatchService
{
	Task<IEnumerable<ChickenBatchDto>> GetAllAsync();
	Task<ChickenBatchDto?> GetByIdAsync(int id);
	Task AddAsync(ChickenBatchDto batch);
	Task UpdateAsync(ChickenBatchDto batch);
	Task DeleteAsync(int id);
	Task<IEnumerable<ChickenBatchDto>> GetByFarmerIdAsync(int farmerId);
	Task<IEnumerable<ChickenBatchDto>> GetByDateAsync(DateTime date);
}
