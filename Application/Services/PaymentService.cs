using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;
public class PaymentService : IPaymentService
{
	private readonly IUnitOfWork _unitOfWork;

	public PaymentService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<PaymentDto>> GetAllAsync()
	{
		var rel= await _unitOfWork.Payments.GetAllAsync();
		return rel.Select(p => new PaymentDto
		{
			PaymentId = p.Id,
			OrderId = p.OrderId,
			ShopId = p.ShopId,
			AmountPaid = p.AmountPaid,
			PaymentDate = p.PaymentDate
		}).ToList();
	}

	public async Task<PaymentDto?> GetByIdAsync(int id)
	{
		var rel= await _unitOfWork.Payments.GetByIdAsync(id);
		if (rel == null) return null;
		return new PaymentDto
		{
			PaymentId = rel.Id,
			OrderId = rel.OrderId,
			ShopId = rel.ShopId,
			AmountPaid = rel.AmountPaid,
			PaymentDate = rel.PaymentDate
		};
	}

	public async Task<PaymentDto> CreateAsync(PaymentDto payment)
	{
		var order = await _unitOfWork.Orders.GetByIdAsync(payment.OrderId);
		if (order == null) throw new Exception("Invalid order");

		// Update order payment details
		order.PaidAmount += payment.AmountPaid;
		order.BalanceAmount = order.TotalAmount - order.PaidAmount;

		_unitOfWork.Orders.UpdateAsync(order);

		var pay = new Payment
		{
			OrderId = payment.OrderId,
			ShopId = payment.ShopId,
			AmountPaid = payment.AmountPaid,
			PaymentDate = payment.PaymentDate
		};
		_unitOfWork.Payments.AddAsync(pay);

		await _unitOfWork.Orders.SaveChangesAsync(); // Ensure both updated
		await _unitOfWork.Payments.SaveChangesAsync();

		return payment;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var payment = await _unitOfWork.Payments.GetByIdAsync(id);
		if (payment == null) return false;

		var order = await _unitOfWork.Orders.GetByIdAsync(payment.OrderId);
		if (order != null)
		{
			order.PaidAmount -= payment.AmountPaid;
			order.BalanceAmount = order.TotalAmount - order.PaidAmount;
			_unitOfWork.Orders.UpdateAsync(order);
			await _unitOfWork.Orders.SaveChangesAsync();
		}

		_unitOfWork.Payments.DeleteAsync(payment);
		return await _unitOfWork.Payments.SaveChangesAsync();
	}

	public async Task<IEnumerable<PaymentDto>> GetPaymentsByShopAsync(int shopId)
	{
		var payments = await _unitOfWork.Payments.GetAllAsync();
		var rel= payments.Where(p => p.ShopId == shopId).ToList();
		return rel.Select(p => new PaymentDto
		{
			PaymentId = p.Id,
			OrderId = p.OrderId,
			ShopId = p.ShopId,
			AmountPaid = p.AmountPaid,
			PaymentDate = p.PaymentDate
		}).ToList();
	}

	public async Task<IEnumerable<PaymentDto>> GetPaymentsByOrderAsync(int orderId)
	{
		var payments = await _unitOfWork.Payments.GetAllAsync();
		var rel= payments.Where(p => p.OrderId == orderId).ToList();
		return rel.Select(p => new PaymentDto
		{
			PaymentId = p.Id,
			OrderId = p.OrderId,
			ShopId = p.ShopId,
			AmountPaid = p.AmountPaid,
			PaymentDate = p.PaymentDate
		}).ToList();
	}

	public async Task<decimal> GetTotalPaidByShopAsync(int shopId)
	{
		var payments = await GetPaymentsByShopAsync(shopId);
		return payments.Sum(p => p.AmountPaid);
	}
}
