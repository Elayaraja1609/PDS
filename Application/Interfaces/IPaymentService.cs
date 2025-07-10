using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;
public interface IPaymentService
{
	Task<IEnumerable<PaymentDto>> GetAllAsync();
	Task<PaymentDto?> GetByIdAsync(int id);
	Task<PaymentDto> CreateAsync(PaymentDto payment);
	Task<bool> DeleteAsync(int id);

	Task<IEnumerable<PaymentDto>> GetPaymentsByShopAsync(int shopId);
	Task<IEnumerable<PaymentDto>> GetPaymentsByOrderAsync(int orderId);
	Task<decimal> GetTotalPaidByShopAsync(int shopId);
}
