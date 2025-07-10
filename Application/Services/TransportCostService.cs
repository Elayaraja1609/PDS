using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class TransportCostService : ITransportCostService
{
	private readonly IUnitOfWork _unitOfWork;

	public TransportCostService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<TransportCost>> GetAllAsync()
	{
		return await _unitOfWork.TransportCosts.GetAllAsync();
	}

	public async Task<TransportCost?> GetByIdAsync(int id)
	{
		return await _unitOfWork.TransportCosts.GetByIdAsync(id);
	}

	public async Task<TransportCost> CreateAsync(TransportCost cost)
	{
		_unitOfWork.TransportCosts.AddAsync(cost);
		await _unitOfWork.TransportCosts.SaveChangesAsync();
		return cost;
	}

	public async Task<bool> UpdateAsync(TransportCost cost)
	{
		_unitOfWork.TransportCosts.UpdateAsync(cost);
		return await _unitOfWork.TransportCosts.SaveChangesAsync();
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var existing = await _unitOfWork.TransportCosts.GetByIdAsync(id);
		if (existing == null) return false;

		_unitOfWork.TransportCosts.DeleteAsync(existing);
		return await _unitOfWork.TransportCosts.SaveChangesAsync();
	}

	public async Task<IEnumerable<TransportCost>> GetByDateAsync(DateTime date)
	{
		var all = await _unitOfWork.TransportCosts.GetAllAsync();
		return all.Where(c => c.Date.Date == date.Date).ToList();
	}

	public async Task<IEnumerable<TransportCost>> GetByVehicleAsync(int vehicleId)
	{
		var all = await _unitOfWork.TransportCosts.GetAllAsync();
		return all.Where(c => c.VehicleId == vehicleId).ToList();
	}

	public async Task<decimal> GetTotalCostByDateAsync(DateTime date)
	{
		var costs = await GetByDateAsync(date);
		return costs.Sum(c => c.Amount);
	}

	public async Task<decimal> GetTotalCostByVehicleAsync(int vehicleId)
	{
		var costs = await GetByVehicleAsync(vehicleId);
		return costs.Sum(c => c.Amount);
	}
}

