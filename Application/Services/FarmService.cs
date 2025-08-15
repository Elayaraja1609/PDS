using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class FarmService: IFarmService
{
	private readonly IUnitOfWork _unitOfWork;
	public FarmService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<FarmDto?> GetFarmByIdAsync(int id)
	{
		var rel= await _unitOfWork.Farms.GetByIdAsync(id);
		if (rel == null) return null;
		return new FarmDto
		{
			FramId = rel.Id,
			Name = rel.Name,
			Location = rel.Location,
			ContactPerson = rel.ContactPerson,
			PhoneNumber = rel.PhoneNumber,
			//FarmStocks = rel.FarmStocks?.Select(fs => new FarmStockDto
			//{
			//	FarmStockId = fs.Id,
			//	Date = fs.Date,
			//	TotalAvailableInKg = fs.TotalAvailableInKg,
			//	RemainingAvailableInKg = fs.RemainingAvailableInKg
			//	// Map other properties as needed
			//}).ToList()
		};
	}
	public async Task<IReadOnlyList<FarmDto>> GetAllFarmsAsync()
	{
		var rel= await _unitOfWork.Farms.GetAllAsync();
		return rel.Select(farm => new FarmDto
		{
			FramId = farm.Id,
			Name = farm.Name,
			Location = farm.Location,
			ContactPerson = farm.ContactPerson,
			PhoneNumber = farm.PhoneNumber,
			//FarmStocks = farm.FarmStocks?.Select(fs => new FarmStockDto
			//{
			//	FarmStockId = fs.Id,
			//	Date = fs.Date,
			//	TotalAvailableInKg = fs.TotalAvailableInKg,
			//	RemainingAvailableInKg = fs.RemainingAvailableInKg
			//}).ToList()
		}).ToList();
	}
	public async Task AddFarmAsync(FarmDto farm)
	{
		Farm farm1 = new Farm
		{
			Name = farm.Name,
			Location = farm.Location,
			ContactPerson = farm.ContactPerson,
			PhoneNumber = farm.PhoneNumber,
		};
		_unitOfWork.Farms.AddAsync(farm1);
		await _unitOfWork.CompleteAsync();
	}
	public async Task UpdateFarmAsync(FarmDto farm)
	{
		var existingFarm = await GetFarmByIdAsync(farm.FramId);
		if (existingFarm == null)
			throw new Exception("Farm not found");
		var farm1 = new Farm
		{
			Id = farm.FramId,
			Name = farm.Name,
			Location = farm.Location,
			ContactPerson = farm.ContactPerson,
			PhoneNumber = farm.PhoneNumber
		};
		_unitOfWork.Farms.UpdateAsync(farm1);
		await _unitOfWork.CompleteAsync();
	}
	public async Task DeleteFarmAsync(int id)
	{
		var existingFarm = await GetFarmByIdAsync(id);
		if (existingFarm != null)
		{
			var farm = new Farm
			{
				Id = id,
				Name = existingFarm.Name,
				Location = existingFarm.Location,
				ContactPerson = existingFarm.ContactPerson,
				PhoneNumber = existingFarm.PhoneNumber
			};
			_unitOfWork.Farms.DeleteAsync(farm);
			await _unitOfWork.CompleteAsync();
		}
	}
	public async Task<bool> FarmExistsAsync(int id)
	{
		return true;
		//return await Task.FromResult(_unitOfWork.Farms.IsExist(id));
	}
	//public async Task<IReadOnlyList<FarmDto>> GetFarmsWithSpecAsync(ISpecification<FarmDto> spec)
	//{
	//	var rel= await _unitOfWork.Farms.GetAllWithSpec(spec);
	//	return rel.Select(farm => new FarmDto
	//	{
	//		FramId = farm.Id,
	//		Name = farm.Name,
	//		Location = farm.Location,
	//		ContactPerson = farm.ContactPerson,
	//		PhoneNumber = farm.PhoneNumber,
	//		FarmStocks = farm.FarmStocks?.Select(fs => new FarmStockDto
	//		{
	//			FarmStockId = fs.Id,
	//			Date = fs.Date,
	//			TotalAvailableInKg = fs.TotalAvailableInKg,
	//			RemainingAvailableInKg = fs.RemainingAvailableInKg
	//		}).ToList()
	//	}).ToList();
	//}
	//public async Task<int> CountFarmsWithSpecAsync(ISpecification<FarmDto> spec)
	//{
	//	return await _unitOfWork.Farms.CountAsync(spec);
	//}
}
