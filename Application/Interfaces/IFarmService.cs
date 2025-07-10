using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IFarmService
{
	Task<FarmDto?> GetFarmByIdAsync(int id);
	Task<IReadOnlyList<FarmDto>> GetAllFarmsAsync();
	Task AddFarmAsync(FarmDto farm);
	Task UpdateFarmAsync(FarmDto farm);
	Task DeleteFarmAsync(int id);
	Task<bool> FarmExistsAsync(int id);
	//Task<IReadOnlyList<FarmDto>> GetFarmsWithSpecAsync(ISpecification<FarmDto> spec);
	//Task<int> CountFarmsWithSpecAsync(ISpecification<FarmDto> spec);
}
