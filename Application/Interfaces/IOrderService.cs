using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;
public interface IOrderService
{
	Task<IEnumerable<OrderDto>> GetAllAsync();
	Task<OrderDto?> GetByIdAsync(int id);
	Task<OrderDto> CreateAsync(OrderDto order);
	Task<bool> UpdateAsync(OrderDto order);
	Task<bool> DeleteAsync(int id);

	Task<IEnumerable<OrderDto>> GetOrdersByShopAsync(int shopId);
	Task<IEnumerable<OrderDto>> GetOrdersByDateAsync(DateTime date);
	Task<IEnumerable<OrderDto>> GetOrdersByDeliveryAsync(int deliveryId);

	Task<decimal> GetTotalAmountAsync(int orderId);
	Task<decimal> GetBalanceAmountAsync(int orderId);
	Task<bool> RecordPaymentAsync(int orderId, decimal amount);
}
