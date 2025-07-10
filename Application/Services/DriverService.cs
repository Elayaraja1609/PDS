using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;
public class DriverService : IDriverService
{
	private readonly IUnitOfWork _unitOfWork;

	public DriverService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<Driver>> GetAllAsync()
	{
		return await _unitOfWork.Drivers.GetAllAsync();
	}

	public async Task<Driver?> GetByIdAsync(int id)
	{
		return await _unitOfWork.Drivers.GetByIdAsync(id);
	}

	public async Task<Driver> CreateAsync(Driver driver)
	{
		_unitOfWork.Drivers.AddAsync(driver);
		await _unitOfWork.Drivers.SaveChangesAsync();
		return driver;
	}

	public async Task<bool> UpdateAsync(Driver driver)
	{
		_unitOfWork.Drivers.UpdateAsync(driver);
		return await _unitOfWork.Drivers.SaveChangesAsync();
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var existing = await _unitOfWork.Drivers.GetByIdAsync(id);
		if (existing == null) return false;

		_unitOfWork.Drivers.DeleteAsync(existing);
		return await _unitOfWork.Drivers.SaveChangesAsync();
	}

	public async Task<bool> IsAvailableAsync(int driverId, DateTime date)
	{
		var deliveries = await _unitOfWork.Deliverys.GetAllAsync();
		return !deliveries.Any(d => d.DriverId == driverId && d.DeliveryDate.Date == date.Date);
	}

	public async Task<IEnumerable<Driver>> GetAvailableDriversAsync(DateTime date)
	{
		var allDrivers = await _unitOfWork.Drivers.GetAllAsync();
		var allDeliveries = await _unitOfWork.Deliverys.GetAllAsync();

		var busyDriverIds = allDeliveries
			.Where(d => d.DeliveryDate.Date == date.Date)
			.Select(d => d.DriverId)
			.Distinct();

		return allDrivers.Where(driver => !busyDriverIds.Contains(driver.Id)).ToList();
	}

	public async Task<IEnumerable<Delivery>> GetDeliveriesByDriverAsync(int driverId, DateTime? date = null)
	{
		var all = await _unitOfWork.Deliverys.GetAllAsync();
		return all
			.Where(d => d.DriverId == driverId && (!date.HasValue || d.DeliveryDate.Date == date.Value.Date))
			.ToList();
	}
}
