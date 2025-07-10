using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;
public class OrderService : IOrderService
{
	private readonly IUnitOfWork _unitOfWork;

	public OrderService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<OrderDto>> GetAllAsync()
	{
		var rel= await _unitOfWork.Orders.GetAllAsync();
		return rel.Select(o => new OrderDto
		{
			OrderId = o.Id,
			ShopId = o.ShopId,
			OrderDate = o.OrderDate,
			QuantityInKg = o.QuantityInKg,
			RatePerKg = o.RatePerKg,
			TotalAmount = o.TotalAmount,
			PaidAmount = o.PaidAmount,
			BalanceAmount = o.BalanceAmount
		}).ToList();
	}

	public async Task<OrderDto?> GetByIdAsync(int id)
	{
		var rel= await _unitOfWork.Orders.GetByIdAsync(id);
		if (rel == null) return null;
		return new OrderDto
		{
			OrderId = rel.Id,
			ShopId = rel.ShopId,
			OrderDate = rel.OrderDate,
			QuantityInKg = rel.QuantityInKg,
			RatePerKg = rel.RatePerKg,
			TotalAmount = rel.TotalAmount,
			PaidAmount = rel.PaidAmount,
			BalanceAmount = rel.BalanceAmount
		};
	}

	public async Task<OrderDto> CreateAsync(OrderDto orderDto)
	{
		// Compute totals
		//order.TotalAmount = Convert.ToDecimal(order.QuantityInKg) * order.RatePerKg;
		//order.BalanceAmount = order.TotalAmount - order.PaidAmount;
		var order = new Order()
		{
			OrderDate = orderDto.OrderDate,
			QuantityInKg = orderDto.QuantityInKg,
			RatePerKg = orderDto.RatePerKg,
			TotalAmount = orderDto.TotalAmount,//Convert.ToDecimal(orderDto.QuantityInKg) * orderDto.RatePerKg,
			PaidAmount = orderDto.PaidAmount,
			BalanceAmount = orderDto.BalanceAmount,//(Convert.ToDecimal(orderDto.QuantityInKg) * orderDto.RatePerKg) - orderDto.PaidAmount,
			ShopId = orderDto.ShopId
		};
		_unitOfWork.Orders.AddAsync(order);
		var rel = await _unitOfWork.Orders.SaveChangesAsync();
		if (!rel) throw new Exception("Failed to create order");
		orderDto.OrderId = order.Id;
		return orderDto;
	}

	public async Task<bool> UpdateAsync(OrderDto order)
	{
		// Recalculate in case values changed
		//order.TotalAmount = Convert.ToDecimal(order.QuantityInKg) * order.RatePerKg;
		//order.BalanceAmount = order.TotalAmount - order.PaidAmount;

		var existingOrder = await _unitOfWork.Orders.GetByIdAsync(order.OrderId);
		if (existingOrder == null) return false;
		existingOrder.OrderDate = order.OrderDate;
		existingOrder.QuantityInKg = order.QuantityInKg;
		existingOrder.RatePerKg = order.RatePerKg;
		existingOrder.TotalAmount = order.TotalAmount; // Convert.ToDecimal(order.QuantityInKg) * order.RatePerKg;
		existingOrder.PaidAmount = order.PaidAmount;
		existingOrder.BalanceAmount = order.BalanceAmount; // (Convert.ToDecimal(order.QuantityInKg) * order.RatePerKg) - order.PaidAmount;
		existingOrder.ShopId = order.ShopId;

		_unitOfWork.Orders.UpdateAsync(existingOrder);
		return await _unitOfWork.Orders.SaveChangesAsync();
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var order = await _unitOfWork.Orders.GetByIdAsync(id);
		if (order == null) return false;

		_unitOfWork.Orders.DeleteAsync(order);
		return await _unitOfWork.Orders.SaveChangesAsync();
	}

	public async Task<IEnumerable<OrderDto>> GetOrdersByShopAsync(int shopId)
	{
		var all = await _unitOfWork.Orders.GetAllAsync();
		var rel= all.Where(o => o.ShopId == shopId).ToList();
		return rel.Select(o => new OrderDto
		{
			OrderId = o.Id,
			ShopId = o.ShopId,
			OrderDate = o.OrderDate,
			QuantityInKg = o.QuantityInKg,
			RatePerKg = o.RatePerKg,
			TotalAmount = o.TotalAmount,
			PaidAmount = o.PaidAmount,
			BalanceAmount = o.BalanceAmount
		}).ToList();
	}

	public async Task<IEnumerable<OrderDto>> GetOrdersByDateAsync(DateTime date)
	{
		var all = await _unitOfWork.Orders.GetAllAsync();
		var rel= all.Where(o => o.OrderDate.Date == date.Date).ToList();
		return rel.Select(o => new OrderDto
		{
			OrderId = o.Id,
			ShopId = o.ShopId,
			OrderDate = o.OrderDate,
			QuantityInKg = o.QuantityInKg,
			RatePerKg = o.RatePerKg,
			TotalAmount = o.TotalAmount,
			PaidAmount = o.PaidAmount,
			BalanceAmount = o.BalanceAmount
		}).ToList();
	}

	public async Task<IEnumerable<OrderDto>> GetOrdersByDeliveryAsync(int deliveryId)
	{
		var all = await _unitOfWork.Orders.GetAllAsync();
		var rel= all.Where(o => o.DeliveryId == deliveryId).ToList();
		return rel.Select(o => new OrderDto
		{
			OrderId = o.Id,
			ShopId = o.ShopId,
			OrderDate = o.OrderDate,
			QuantityInKg = o.QuantityInKg,
			RatePerKg = o.RatePerKg,
			TotalAmount = o.TotalAmount,
			PaidAmount = o.PaidAmount,
			BalanceAmount = o.BalanceAmount
		}).ToList();
	}

	public async Task<decimal> GetTotalAmountAsync(int orderId)
	{
		var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
		return order?.TotalAmount ?? 0;
	}

	public async Task<decimal> GetBalanceAmountAsync(int orderId)
	{
		var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
		return order?.BalanceAmount ?? 0;
	}

	public async Task<bool> RecordPaymentAsync(int orderId, decimal amount)
	{
		var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
		if (order == null) return false;

		order.PaidAmount += amount;
		order.BalanceAmount = order.TotalAmount - order.PaidAmount;

		_unitOfWork.Orders.UpdateAsync(order);
		return await _unitOfWork.Orders.SaveChangesAsync();
	}
}
