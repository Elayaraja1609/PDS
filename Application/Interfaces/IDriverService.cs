using Domain.Entities;

namespace Application.Interfaces;
public interface IDriverService
{
	Task<IEnumerable<Driver>> GetAllAsync();
	Task<Driver?> GetByIdAsync(int id);
	Task<Driver> CreateAsync(Driver driver);
	Task<bool> UpdateAsync(Driver driver);
	Task<bool> DeleteAsync(int id);

	Task<bool> IsAvailableAsync(int driverId, DateTime date);
	Task<IEnumerable<Driver>> GetAvailableDriversAsync(DateTime date);
	Task<IEnumerable<Delivery>> GetDeliveriesByDriverAsync(int driverId, DateTime? date = null);
}
