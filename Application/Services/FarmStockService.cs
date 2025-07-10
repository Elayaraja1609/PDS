using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;
public class FarmStockService : IFarmStockService
{
	private readonly IUnitOfWork _unitOfWork;

	public FarmStockService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<FarmStockDto>> GetAllAsync()
	{
		var rel= await _unitOfWork.FarmStocks.GetAllAsync();
		return rel.Select(x => new FarmStockDto
		{
			FarmStockId = x.Id,
			Date = x.Date,
			QuantityAvailableInKg = x.QuantityAvailableInKg,
			//RemainingAvailableInKg = x.RemainingAvailableInKg
			NoOfChickens = x.NoOfChickens
		});
	}

	public async Task<FarmStockDto?> GetByIdAsync(int id)
	{
		var rel= await _unitOfWork.FarmStocks.GetByIdAsync(id);
		if (rel == null) return null;
		return new FarmStockDto
		{
			FarmStockId = rel.Id,
			Date = rel.Date,
			QuantityAvailableInKg = rel.QuantityAvailableInKg,
			//RemainingAvailableInKg = rel.RemainingAvailableInKg
			NoOfChickens = rel.NoOfChickens
		};
	}

	public async Task<FarmStockDto> CreateAsync(FarmStockDto stock)
	{
		var farmstock = new FarmStock
		{
			Date = stock.Date,
			QuantityAvailableInKg = stock.QuantityAvailableInKg,
			NoOfChickens = stock.NoOfChickens,
			//RemainingAvailableInKg = stock.RemainingAvailableInKg
		};
		_unitOfWork.FarmStocks.AddAsync(farmstock);
		await _unitOfWork.FarmStocks.SaveChangesAsync();
		return stock;
	}

	public async Task<bool> UpdateAsync(FarmStockDto stock)
	{
		var farmstock = new FarmStock
		{
			Date = stock.Date,
			QuantityAvailableInKg = stock.QuantityAvailableInKg,
			NoOfChickens = stock.NoOfChickens,
			//RemainingAvailableInKg = stock.RemainingAvailableInKg
		};
		_unitOfWork.FarmStocks.UpdateAsync(farmstock);
		return await _unitOfWork.FarmStocks.SaveChangesAsync();
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var existing = await _unitOfWork.FarmStocks.GetByIdAsync(id);
		if (existing == null) return false;

		_unitOfWork.FarmStocks.DeleteAsync(existing);
		return await _unitOfWork.FarmStocks.SaveChangesAsync();
	}

	public async Task<FarmStockDto?> GetLatestStockAsync()
	{
		var all = await _unitOfWork.FarmStocks.GetAllAsync();
		var rel= all.OrderByDescending(x => x.Date).FirstOrDefault();
		if (rel == null) return null;
		return new FarmStockDto
		{
			FarmStockId = rel.Id,
			Date = rel.Date,
			QuantityAvailableInKg = rel.QuantityAvailableInKg,
			NoOfChickens = rel.NoOfChickens,
			//RemainingAvailableInKg = rel.RemainingAvailableInKg
		};
	}

	public async Task<double> GetAvailableStockAsync()
	{
		var latest = await GetLatestStockAsync();
		return latest?.QuantityAvailableInKg ?? 0;
	}

	public async Task<bool> AdjustStockAsync(double quantity, bool increase)
	{
		var stock = await GetLatestStockAsync();
		if (stock == null) return false;

		stock.QuantityAvailableInKg += (increase ? quantity : -quantity);
		var farmstock = new FarmStock
		{
			Date = stock.Date,
			QuantityAvailableInKg = stock.QuantityAvailableInKg,
			NoOfChickens = stock.NoOfChickens,
			//RemainingAvailableInKg = stock.RemainingAvailableInKg
		};
		_unitOfWork.FarmStocks.UpdateAsync(farmstock);
		return await _unitOfWork.FarmStocks.SaveChangesAsync();
	}
}
