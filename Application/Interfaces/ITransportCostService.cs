using Domain.Entities;

namespace Application.Interfaces;
public interface ITransportCostService
{
	Task<IEnumerable<TransportCost>> GetAllAsync();
	Task<TransportCost?> GetByIdAsync(int id);
	Task<TransportCost> CreateAsync(TransportCost cost);
	Task<bool> UpdateAsync(TransportCost cost);
	Task<bool> DeleteAsync(int id);

	Task<IEnumerable<TransportCost>> GetByDateAsync(DateTime date);
	Task<IEnumerable<TransportCost>> GetByVehicleAsync(int vehicleId);
	Task<decimal> GetTotalCostByDateAsync(DateTime date);
	Task<decimal> GetTotalCostByVehicleAsync(int vehicleId);
}
