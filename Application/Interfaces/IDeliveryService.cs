using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;
public interface IDeliveryService
{
	Task<IEnumerable<DeliveryDto>> GetAllAsync();
	Task<DeliveryDto?> GetByIdAsync(int id);
	Task<DeliveryDto> CreateAsync(DeliveryDto delivery);
	Task<bool> UpdateAsync(DeliveryDto delivery);
	Task<bool> DeleteAsync(int id);

	Task<IEnumerable<DeliveryDto>> GetDeliveriesByDateAsync(DateTime date);
	Task<IEnumerable<DeliveryDto>> GetDeliveriesByDriverAsync(int driverId, DateTime? date = null);
	Task<IEnumerable<DeliveryDto>> GetDeliveriesByVehicleAsync(int vehicleId, DateTime? date = null);

	Task<double> GetRemainingLoadInVehicleAsync(int deliveryId);
	Task<bool> AssignOrderToDeliveryAsync(int deliveryId, Order order);
}
