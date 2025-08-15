using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IChickenBatchService
{
	Task<IEnumerable<ChickenBatchDto>> GetAllAsync();
	Task<ChickenBatchDto?> GetByIdAsync(int id);
	Task AddAsync(ChickenBatchDto batch);
	Task UpdateAsync(int id, ChickenBatchDto batch);
	Task DeleteAsync(int id);
}
