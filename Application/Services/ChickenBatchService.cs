using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class ChickenBatchService : IChickenBatchService
{
	private readonly IUnitOfWork _unitOfWork;

	public ChickenBatchService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<ChickenBatchDto>> GetAllAsync()
	{
		var CB = await _unitOfWork.ChickenBatchs.GetAllAsync();
		var farms = await _unitOfWork.Farms.GetAllAsync();
		var farmStock = await _unitOfWork.FarmStocks.GetAllAsync();
		return CB.Select(cb => new ChickenBatchDto
		{
			Id = cb.Id,
			FarmStockId = cb.FarmStockId,
			FarmerId = cb.FarmerId,
			Farm = farms.Where(f => f.Id == cb.FarmerId).Select(f => new FarmDto
			{
				FramId = f.Id,
				Name = f.Name,
				Location = f.Location,
				ContactPerson = f.ContactPerson,
				PhoneNumber = f.PhoneNumber
			}).FirstOrDefault(),
			//FarmStock = farmStock.Where(x=>x.Id == cb.FarmStockId).Select(x=>new FarmStockDto { 
			//	FarmStockId=x.Id,
			//	Date=x.Date,
			//	TotalAvailableInKg=x.TotalAvailableInKg,
			//	RemainingAvailableInKg = x.RemainingAvailableInKg
			//}).FirstOrDefault(),
			CollectionDate = cb.CollectionDate,
			QuantityInKg = cb.QuantityInKg,
			NumberOfChickens = cb.NumberOfChickens,
			Type = cb.Type,
			Status = cb.Status,
		}).ToList();
	}

	public async Task<ChickenBatchDto?> GetByIdAsync(int id)
	{
		var cb = await _unitOfWork.ChickenBatchs.GetByIdAsync(id);
		var farms = await _unitOfWork.Farms.GetAllAsync();
		var farmStock = await _unitOfWork.FarmStocks.GetAllAsync();
		if (cb == null) return null;
		return new ChickenBatchDto
		{
			Id = cb.Id,
			FarmStockId = cb.FarmStockId,
			FarmerId = cb.FarmerId,
			Farm = farms.Where(f => f.Id == cb.FarmerId).Select(f => new FarmDto
			{
				FramId = f.Id,
				Name = f.Name,
				Location = f.Location,
				ContactPerson = f.ContactPerson,
				PhoneNumber = f.PhoneNumber
			}).FirstOrDefault(),
			//FarmStock = farmStock.Where(x => x.Id == cb.FarmStockId).Select(x => new FarmStockDto
			//{
			//	FarmStockId = x.Id,
			//	Date = x.Date,
			//	TotalAvailableInKg = x.TotalAvailableInKg,
			//	RemainingAvailableInKg = x.RemainingAvailableInKg
			//}).FirstOrDefault(),
			CollectionDate = cb.CollectionDate,
			QuantityInKg = cb.QuantityInKg,
			NumberOfChickens = cb.NumberOfChickens,
			Type = cb.Type,
			Status = cb.Status,
		};
	}

	public async Task AddAsync(ChickenBatchDto batch)
	{
		var farmStock = await _unitOfWork.FarmStocks.GetByIdAsync(batch.FarmStockId);
		if (farmStock == null)
			throw new Exception("Invalid FarmStock ID");

		// Add batch quantity to total available in stock
		farmStock.QuantityAvailableInKg += batch.QuantityInKg;
		var chickenBatch = new ChickenBatch
		{
			FarmStockId = batch.FarmStockId,
			FarmerId = batch.FarmerId,
			CollectionDate = batch.CollectionDate,
			QuantityInKg = batch.QuantityInKg,
			NumberOfChickens = batch.NumberOfChickens,
			Type = batch.Type,
			Status = batch.Status
		};
		_unitOfWork.ChickenBatchs.AddAsync(chickenBatch);
		_unitOfWork.FarmStocks.UpdateAsync(farmStock);
		await _unitOfWork.ChickenBatchs.SaveChangesAsync();
	}

	public async Task UpdateAsync(ChickenBatchDto batch)
	{
		var chickenBatch = new ChickenBatch
		{
			Id=batch.Id,
			FarmStockId = batch.FarmStockId,
			FarmerId = batch.FarmerId,
			CollectionDate = batch.CollectionDate,
			QuantityInKg = batch.QuantityInKg,
			NumberOfChickens = batch.NumberOfChickens,
			Type = batch.Type,
			Status = batch.Status
		};
		_unitOfWork.ChickenBatchs.UpdateAsync(chickenBatch);
		await _unitOfWork.ChickenBatchs.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var batch = await _unitOfWork.ChickenBatchs.GetByIdAsync(id);
		if (batch == null) throw new Exception("Batch not found");

		var farmStock = await _unitOfWork.FarmStocks.GetByIdAsync(batch.FarmStockId);
		if (farmStock != null)
		{
			farmStock.QuantityAvailableInKg -= batch.QuantityInKg;
			_unitOfWork.FarmStocks.UpdateAsync(farmStock);
		}

		_unitOfWork.ChickenBatchs.DeleteAsync(batch);
		await _unitOfWork.ChickenBatchs.SaveChangesAsync();
	}

	public async Task<IEnumerable<ChickenBatchDto>> GetByFarmerIdAsync(int farmerId)
	{
		var spec = new ChickenBatchByFarmerSpec(farmerId);
		var rel= await _unitOfWork.ChickenBatchs.GetAllWithSpec(spec);
		var farms = await _unitOfWork.Farms.GetAllAsync();
		var farmStock = await _unitOfWork.FarmStocks.GetAllAsync();
		return rel.Select(cb => new ChickenBatchDto
		{
			Id = cb.Id,
			FarmStockId = cb.FarmStockId,
			FarmerId = cb.FarmerId,
			Farm = farms.Where(f => f.Id == cb.FarmerId).Select(f => new FarmDto
			{
				FramId = f.Id,
				Name = f.Name,
				Location = f.Location,
				ContactPerson = f.ContactPerson,
				PhoneNumber = f.PhoneNumber
			}).FirstOrDefault(),
			//FarmStock = farmStock.Where(x => x.Id == cb.FarmStockId).Select(x => new FarmStockDto
			//{
			//	FarmStockId = x.Id,
			//	Date = x.Date,
			//	TotalAvailableInKg = x.TotalAvailableInKg,
			//	RemainingAvailableInKg = x.RemainingAvailableInKg
			//}).FirstOrDefault(),
			CollectionDate = cb.CollectionDate,
			QuantityInKg = cb.QuantityInKg,
			NumberOfChickens = cb.NumberOfChickens,
			Type = cb.Type,
			Status = cb.Status
		}).ToList();
	}

	public async Task<IEnumerable<ChickenBatchDto>> GetByDateAsync(DateTime date)
	{
		var spec = new ChickenBatchByDateSpec(date);
		var rel= await _unitOfWork.ChickenBatchs.GetAllWithSpec(spec);
		var farms = await _unitOfWork.Farms.GetAllAsync();
		var farmStock = await _unitOfWork.FarmStocks.GetAllAsync();
		return rel.Select(cb => new ChickenBatchDto
		{
			Id = cb.Id,
			FarmStockId = cb.FarmStockId,
			FarmerId = cb.FarmerId,
			Farm = farms.Where(f => f.Id == cb.FarmerId).Select(f => new FarmDto
			{
				FramId = f.Id,
				Name = f.Name,
				Location = f.Location,
				ContactPerson = f.ContactPerson,
				PhoneNumber = f.PhoneNumber
			}).FirstOrDefault(),
			//FarmStock = farmStock.Where(x => x.Id == cb.FarmStockId).Select(x => new FarmStockDto
			//{
			//	FarmStockId = x.Id,
			//	Date = x.Date,
			//	TotalAvailableInKg = x.TotalAvailableInKg,
			//	RemainingAvailableInKg = x.RemainingAvailableInKg
			//}).FirstOrDefault(),
			CollectionDate = cb.CollectionDate,
			QuantityInKg = cb.QuantityInKg,
			NumberOfChickens = cb.NumberOfChickens,
			Type = cb.Type,
			Status = cb.Status
		}).ToList();
	}
}

public class ChickenBatchByFarmerSpec : BaseSpecification<ChickenBatch>
{
	public ChickenBatchByFarmerSpec(int farmerId)
		: base(cb => cb.FarmerId == farmerId)
	{
		AddInclude(cb => cb.Farmer);
	}
}
public class ChickenBatchByDateSpec : BaseSpecification<ChickenBatch>
{
	public ChickenBatchByDateSpec(DateTime date)
		: base(cb => cb.CollectionDate.Date == date.Date)
	{
		AddInclude(cb => cb.Farmer);
	}
}
