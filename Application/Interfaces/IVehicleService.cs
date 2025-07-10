using Domain.Entities;

namespace Application.Interfaces;
public interface IVehicleService
{
	Task<IEnumerable<Vehicle>> GetAllAsync();
	Task<Vehicle?> GetByIdAsync(int id);
	Task<Vehicle> CreateAsync(Vehicle vehicle);
	Task<bool> UpdateAsync(Vehicle vehicle);
	Task<bool> DeleteAsync(int id);

	Task<bool> IsAvailableAsync(int vehicleId, DateTime date);
	Task<double> GetRemainingCapacityAsync(int vehicleId, DateTime date);
	Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync(DateTime date);
}
