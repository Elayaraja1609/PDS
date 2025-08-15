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
		return CB.Select(cb => new ChickenBatchDto
		{
			Id = cb.Id,
			FarmerId = cb.FarmerId,
			Farm = farms.Where(f => f.Id == cb.FarmerId).Select(f => new FarmDto
			{
				FramId = f.Id,
				Name = f.Name,
				Location = f.Location,
				ContactPerson = f.ContactPerson,
				PhoneNumber = f.PhoneNumber
			}).FirstOrDefault(),
			CollectionDate = cb.CollectionDate,
			QuantityInKg = cb.QuantityInKg,
			RemainingQuantityInKg = cb.RemainingQuantityInKg,
			NumberOfChickens = cb.NumberOfChickens,
			Type = cb.Type,
			Status = cb.Status,
			Notes = cb.Notes,
			FarmerName = cb.FarmerName,
			CollectionWeight = cb.CollectionWeight,
			CreatedAt = cb.CreatedAt,
			IsActive = cb.IsActive
		}).ToList();
	}

	public async Task<ChickenBatchDto?> GetByIdAsync(int id)
	{
		var cb = await _unitOfWork.ChickenBatchs.GetByIdAsync(id);
		var farms = await _unitOfWork.Farms.GetAllAsync();
		if (cb == null) return null;
		return new ChickenBatchDto
		{
			Id = cb.Id,
			FarmerId = cb.FarmerId,
			Farm = farms.Where(f => f.Id == cb.FarmerId).Select(f => new FarmDto
			{
				FramId = f.Id,
				Name = f.Name,
				Location = f.Location,
				ContactPerson = f.ContactPerson,
				PhoneNumber = f.PhoneNumber
			}).FirstOrDefault(),
			CollectionDate = cb.CollectionDate,
			QuantityInKg = cb.QuantityInKg,
			RemainingQuantityInKg = cb.RemainingQuantityInKg,
			NumberOfChickens = cb.NumberOfChickens,
			Type = cb.Type,
			Status = cb.Status,
			Notes = cb.Notes,
			FarmerName = cb.FarmerName,
			CollectionWeight = cb.CollectionWeight,
			CreatedAt = cb.CreatedAt,
			IsActive = cb.IsActive
		};
	}

	public async Task AddAsync(ChickenBatchDto batch)
	{
		var chickenBatch = new ChickenBatch
		{
			FarmerId = batch.FarmerId,
			CollectionDate = batch.CollectionDate,
			QuantityInKg = batch.QuantityInKg,
			RemainingQuantityInKg = batch.QuantityInKg, // Initially, remaining equals total
			NumberOfChickens = batch.NumberOfChickens,
			Type = batch.Type,
			Status = "InStock",
			Notes = batch.Notes,
			FarmerName = batch.FarmerName,
			CollectionWeight = batch.CollectionWeight,
			CreatedAt = DateTime.UtcNow,
			IsActive = true
		};

		await _unitOfWork.ChickenBatchs.AddAsync(chickenBatch);
		await _unitOfWork.CompleteAsync();
	}

	public async Task UpdateAsync(int id, ChickenBatchDto batch)
	{
		var existingBatch = await _unitOfWork.ChickenBatchs.GetByIdAsync(id);
		if (existingBatch == null)
			throw new Exception("ChickenBatch not found");

		existingBatch.CollectionDate = batch.CollectionDate;
		existingBatch.QuantityInKg = batch.QuantityInKg;
		existingBatch.NumberOfChickens = batch.NumberOfChickens;
		existingBatch.Type = batch.Type;
		existingBatch.Status = batch.Status;
		existingBatch.Notes = batch.Notes;
		existingBatch.FarmerName = batch.FarmerName;
		existingBatch.CollectionWeight = batch.CollectionWeight;

		await _unitOfWork.CompleteAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var batch = await _unitOfWork.ChickenBatchs.GetByIdAsync(id);
		if (batch == null)
			throw new Exception("ChickenBatch not found");

		await _unitOfWork.ChickenBatchs.DeleteAsync(id);
		await _unitOfWork.CompleteAsync();
	}
}
