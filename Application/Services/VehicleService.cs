using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;
public class VehicleService : IVehicleService
{
	
	private readonly IUnitOfWork _unitOfWork;

	public VehicleService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<Vehicle>> GetAllAsync()
	{
		return await _unitOfWork.Vehicles.GetAllAsync();
	}

	public async Task<Vehicle?> GetByIdAsync(int id)
	{
		return await _unitOfWork.Vehicles.GetByIdAsync(id);
	}

	public async Task<Vehicle> CreateAsync(Vehicle vehicle)
	{
		_unitOfWork.Vehicles.AddAsync(vehicle);
		await _unitOfWork.Vehicles.SaveChangesAsync();
		return vehicle;
	}

	public async Task<bool> UpdateAsync(Vehicle vehicle)
	{
		_unitOfWork.Vehicles.UpdateAsync(vehicle);
		return await _unitOfWork.Vehicles.SaveChangesAsync();
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
		if (vehicle == null) return false;

		_unitOfWork.Vehicles.DeleteAsync(vehicle);
		return await _unitOfWork.Vehicles.SaveChangesAsync();
	}

	public async Task<bool> IsAvailableAsync(int vehicleId, DateTime date)
	{
		var spec = new DeliveriesByVehicleAndDateSpec(vehicleId, date);
		var deliveries = await _unitOfWork.Deliverys.GetAllWithSpec(spec);
		return !deliveries.Any();
	}

	public async Task<double> GetRemainingCapacityAsync(int vehicleId, DateTime date)
	{
		var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(vehicleId);
		if (vehicle == null) return 0;

		var spec = new DeliveriesByVehicleAndDateSpec(vehicleId, date);
		var deliveries = await _unitOfWork.Deliverys.GetAllWithSpec(spec);
		var used = deliveries.Sum(d => d.TotalWeightLoaded);

		return vehicle.CapacityInKg - used;
	}

	public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync(DateTime date)
	{
		var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
		var result = new List<Vehicle>();

		foreach (var v in vehicles)
		{
			if (await IsAvailableAsync(v.Id, date))
				result.Add(v);
		}

		return result;
	}
}
public class DeliveriesByVehicleAndDateSpec : BaseSpecification<Delivery>
{
	public DeliveriesByVehicleAndDateSpec(int vehicleId, DateTime date)
		: base(d => d.VehicleId == vehicleId && d.DeliveryDate.Date == date.Date)
	{
	}
}
