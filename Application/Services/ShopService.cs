using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ShopService : IShopService
{
	private readonly IUnitOfWork _unitOfWork;

	public ShopService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<Shop>> GetAllAsync()
	{
		return await _unitOfWork.Shops.GetAllAsync();
	}

	public async Task<Shop?> GetByIdAsync(int id)
	{
		return await _unitOfWork.Shops.GetByIdAsync(id);
	}

	public async Task<Shop> CreateAsync(Shop shop)
	{
		_unitOfWork.Shops.AddAsync(shop);
		await _unitOfWork.Shops.SaveChangesAsync();
		return shop;
	}

	public async Task<bool> UpdateAsync(Shop shop)
	{
		_unitOfWork.Shops.UpdateAsync(shop);
		return await _unitOfWork.Shops.SaveChangesAsync();
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var existing = await _unitOfWork.Shops.GetByIdAsync(id);
		if (existing == null) return false;

		_unitOfWork.Shops.DeleteAsync(existing);
		return await _unitOfWork.Shops.SaveChangesAsync();
	}

	public async Task<IEnumerable<Order>> GetOrdersByShopAsync(int shopId, DateTime? date = null)
	{
		var allOrders = await _unitOfWork.Orders.GetAllAsync();
		return allOrders
			.Where(o => o.ShopId == shopId && (!date.HasValue || o.OrderDate.Date == date.Value.Date))
			.ToList();
	}

	public async Task<decimal> GetTotalDueAmountAsync(int shopId)
	{
		var orders = await GetOrdersByShopAsync(shopId);
		return orders.Sum(o => o.TotalAmount);
	}

	public async Task<decimal> GetTotalPaidAmountAsync(int shopId)
	{
		var orders = await GetOrdersByShopAsync(shopId);
		return orders.Sum(o => o.PaidAmount);
	}

	public async Task<decimal> GetBalanceAmountAsync(int shopId)
	{
		var orders = await GetOrdersByShopAsync(shopId);
		return orders.Sum(o => o.BalanceAmount);
	}
}
