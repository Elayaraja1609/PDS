using Domain.Entities;

namespace Application.Interfaces;
public interface IShopService
{
	Task<IEnumerable<Shop>> GetAllAsync();
	Task<Shop?> GetByIdAsync(int id);
	Task<Shop> CreateAsync(Shop shop);
	Task<bool> UpdateAsync(Shop shop);
	Task<bool> DeleteAsync(int id);

	Task<IEnumerable<Order>> GetOrdersByShopAsync(int shopId, DateTime? date = null);
	Task<decimal> GetTotalDueAmountAsync(int shopId);
	Task<decimal> GetTotalPaidAmountAsync(int shopId);
	Task<decimal> GetBalanceAmountAsync(int shopId);
}
