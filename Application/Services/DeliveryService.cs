using Application.DTOs;
using Application.DTOs.ResponsesDto.cs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;
public class DeliveryService : IDeliveryService
{
	private readonly IUnitOfWork _unitOfWork;

	public DeliveryService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<List<DeliveryListResDto>> GetDeliveryList()
	{
		List<DeliveryListResDto> deliveryListResDto = new List<DeliveryListResDto>();
		// TODO: Update this method to work with new DeliveryDetail structure
		/*
		var datas = await _unitOfWork.DeliveryDetails.ExecuteStoredProcedureAsync<DeliveryDetail>("[dbo].[Deliverylists]");
		
		if (datas == null || !datas.Any())
		{
			return deliveryListResDto;
		}
		foreach (var data in datas) {
			var VehicleData = await _unitOfWork.Vehicles.GetByIdAsync(data.VehicleId);
			var DriverData = await _unitOfWork.Drivers.GetByIdAsync(data.DriverId);
			var ddd = new DeliveryListResDto
			{
				Id = data.Id,
				DeliveryDate = data.DeliveryDate,
				TotalWeightDelivered = data.TotalWeightLoaded - data.RemainingWeight,
				VehicleNo = VehicleData.VehicleNumber,
				DriverName = DriverData.Name,
			};
			deliveryListResDto.Add(ddd);
		}
		*/
		return deliveryListResDto;
	}
	public async Task<DeliveryDetailResDto> GetDeliveryById(int id)
	{
		DeliveryDetailResDto deliveryDetailResDto = new DeliveryDetailResDto();
		// TODO: Update this method to work with new DeliveryDetail structure
		/*
		var datas = await _unitOfWork.DeliveryDetails.ExecuteStoredProcedureAsync<DeliveryDetail>("[dbo].[Deliverylists]");
		var filteredData = datas.Where(x => x.Id == id).ToList();
		if (datas == null || !datas.Any())
		{
			return deliveryDetailResDto;
		}

		foreach (var data in filteredData)
		{
			var VehicleData = await _unitOfWork.Vehicles.GetByIdAsync(data.VehicleId);
			var DriverData = await _unitOfWork.Drivers.GetByIdAsync(data.DriverId);
			var FarmStockData = await _unitOfWork.FarmStocks.GetByIdAsync(data.FarmStockId);
			var allOrders = await _unitOfWork.Orders.GetAllAsync();
			var ordersbyDeliveryId = allOrders.Where(o => o.DeliveryId == data.Id).ToList();
			var allShops = await _unitOfWork.Shops.GetAllAsync();
			//var ddd = new DeliveryDetailResDto
			//{
			deliveryDetailResDto.DeliveryId = data.Id;
			deliveryDetailResDto.DeliveryDate = data.DeliveryDate;
			deliveryDetailResDto.Area = data.Area;
			deliveryDetailResDto.TotalWeightLoaded = data.TotalWeightLoaded;
			deliveryDetailResDto.RemainingWeight = data.RemainingWeight;
			deliveryDetailResDto.VehicleId = data.VehicleId;
			deliveryDetailResDto.Vehicle = new VehicleResDto()
			{
				Id = data.VehicleId,
				VehicleNumber = VehicleData.VehicleNumber,
				Type = VehicleData.Type,
				CapacityInKg = VehicleData.CapacityInKg,
			};
			deliveryDetailResDto.DriverId = data.DriverId;
			deliveryDetailResDto.Driver = new DriverResDto()
			{
				Id = data.DriverId,
				Name = DriverData.Name,
				PhoneNumber = DriverData.PhoneNumber,
			};
			deliveryDetailResDto.FarmStockId = data.FarmStockId;
			deliveryDetailResDto.FarmStock = new FarmStockResDto()
			{
				Id = data.FarmStockId,
				Date = FarmStockData.Date,
				QuantityAvailableInKg = FarmStockData.QuantityAvailableInKg,
				NoOfChickens = FarmStockData.NoOfChickens,
			};
			deliveryDetailResDto.Orders = ordersbyDeliveryId.Select(o => new OrderResDto
			{
				Id = o.Id,
				OrderDate = o.OrderDate,
				QuantityInKg = o.QuantityInKg,
				ShopId = o.ShopId,
				Shop = new ShopResDto
				{
					Id = o.ShopId,
					ShopName = allShops.Where(s => s.Id == o.ShopId).Select(s => s.ShopName).FirstOrDefault() ?? "Unknown",
					ContactNumber = allShops.Where(s => s.Id == o.ShopId).Select(s => s.ContactNumber).FirstOrDefault() ?? "Unknown",
					Address = allShops.Where(s => s.Id == o.ShopId).Select(s => s.Address).FirstOrDefault() ?? "Unknown"
				},
				RatePerKg = o.RatePerKg,
				PaidAmount = o.PaidAmount,
				BalanceAmount = o.BalanceAmount,
				TotalAmount = o.TotalAmount
			}).ToList();
			};
		*/
		
		return deliveryDetailResDto;
	}
	public async Task<DeliveryUpdatesResDto> CreateDeliveryAsync() {
		DeliveryUpdatesResDto delivery = new DeliveryUpdatesResDto();
		var paramobject = new DeliveriesDto { id = 0, deliverydate = DateTime.Now, area = "test", totalweightloaded = 0, remainingweight = 0, vehicleid = 1, driverid = 1, farmstockid = 1 };//{ id=0, deliverydate= new DateTime(), area="test", totalweightloaded=0, remainingweight=0, vehicleid=1, driverid=1, farmstockid=1 };
		var datas = await _unitOfWork.DeliveryDetails.ExecuteStoredProcedureAsync<DeliveryUpdatesResDto>("[dbo].[UpdateDelivery]", paramobject);
		delivery.DeliveryId= datas.Select(x=>x.DeliveryId).FirstOrDefault();
		return delivery;
	}
	public async Task<IList<DeliveryListResDto>> GetAll()
	{
		IList<DeliveryListResDto> deliveryListResDto = new List<DeliveryListResDto>();
		// TODO: Update this method to work with new DeliveryDetail structure
		/*
		var datas = await _unitOfWork.DeliveryDetails.ExecuteStoredProcedureAsync<DeliveryDetail>("[dbo].[Deliverylists]");
		
		if (datas == null || !datas.Any())
		{
			return deliveryListResDto;
		}
		foreach (var data in datas) {
			var VehicleData = await _unitOfWork.Vehicles.GetByIdAsync(data.VehicleId);
			var DriverData = await _unitOfWork.Drivers.GetByIdAsync(data.DriverId);
			var ddd = new DeliveryListResDto
			{
				Id = data.Id,
				DeliveryDate = data.DeliveryDate,
				TotalWeightDelivered = data.TotalWeightLoaded - data.RemainingWeight,
				VehicleNo = VehicleData.VehicleNumber,
				DriverName = DriverData.Name,
			};
			deliveryListResDto.Add(ddd);
		}
		*/
		return deliveryListResDto;
	}

	public async Task<IEnumerable<DeliveryDto>> GetAllAsync()
	{
		var deliveries = await _unitOfWork.Deliverys.GetAllAsync();
		var deliveryDtos = new List<DeliveryDto>();
		
		foreach (var delivery in deliveries)
		{
			deliveryDtos.Add(new DeliveryDto
			{
				DeliveryId = delivery.Id,
				DeliveryDate = delivery.DeliveryDate,
				Area = delivery.Area,
				TotalWeightLoaded = delivery.TotalWeightLoaded,
				RemainingWeight = delivery.RemainingWeight,
				TotalAmount = delivery.TotalAmount,
				PricePerKg = delivery.PricePerKg,
				VehicleId = delivery.VehicleId,
				DriverId = delivery.DriverId,
				AssistantId = delivery.AssistantId,
				Route = delivery.Route,
				Status = delivery.Status,
				StartTime = delivery.StartTime,
				EndTime = delivery.EndTime
			});
		}
		
		return deliveryDtos;
	}

	public async Task<DeliveryDto?> GetByIdAsync(int id)
	{
		var rel= await _unitOfWork.Deliverys.GetByIdAsync(id);
		if (rel == null) return null;
		return new DeliveryDto
		{
			DeliveryId = rel.Id,
			DriverId = rel.DriverId,
			VehicleId = rel.VehicleId,
			DeliveryDate = rel.DeliveryDate,
			TotalWeightLoaded = rel.TotalWeightLoaded,
			RemainingWeight = rel.RemainingWeight,
			//FarmStockId = rel.FarmStockId,
			//Orders = rel.Orders is null ? null : rel.Orders.Select(o => new OrderDto
			//{
			//	OrderId = o.Id,
			//	OrderDate = o.OrderDate,
			//	QuantityInKg = o.QuantityInKg,
			//	ShopId = o.ShopId,
			//	RatePerKg = o.RatePerKg,
			//	PaidAmount = o.PaidAmount,
			//	BalanceAmount = o.BalanceAmount,
			//	TotalAmount = o.TotalAmount,
			//}).ToList(),
			TransportCosts = rel.TransportCosts is null ? null : rel.TransportCosts.Select(t => new TransportCost
			{
				Id = t.Id,
				VehicleId = t.VehicleId,
				ExpenseType = t.ExpenseType,
				Amount = t.Amount,
				Notes = t.Notes
			}).ToList()
		};
	}

	public async Task<DeliveryDto> CreateAsync(DeliveryDto delivery)
	{
		var delv = new Delivery();
		delv.DeliveryDate = delivery.DeliveryDate;
		delv.Area = delivery.Area;
		delv.TotalWeightLoaded = delivery.TotalWeightLoaded;
		delv.RemainingWeight= delivery.RemainingWeight;
		delv.VehicleId = delivery.VehicleId;
		delv.DriverId = delivery.DriverId;
		//delv.FarmStockId = delivery.FarmStockId;
		//foreach (var item in delivery.Orders) {
		//	var newOrder = new Order()
		//	{
		//		Id = item.OrderId,
		//		QuantityInKg = item.QuantityInKg,
		//		RatePerKg = item.RatePerKg,
		//		TotalAmount = item.TotalAmount,
		//		PaidAmount = item.PaidAmount,
		//		BalanceAmount = item.BalanceAmount,
		//		ShopId = item.ShopId
		//	};
		//	delv.Orders.Add(newOrder);
		//}
		foreach (var t in delivery.TransportCosts) {
			var costData = new TransportCost()
			{
				Id =t.Id,
				VehicleId = t.VehicleId,
				ExpenseType = t.ExpenseType,
				Amount = t.Amount,
				Notes = t.Notes
			};
			delv.TransportCosts.Add(costData);
		}

		_unitOfWork.Deliverys.AddAsync(delv);
		await _unitOfWork.Deliverys.SaveChangesAsync();
		return delivery;
	}

	public async Task<bool> UpdateAsync(DeliveryDto delivery)
	{
		var existing = await _unitOfWork.Deliverys.GetByIdAsync(delivery.DeliveryId);
		if (existing == null) return false;

		existing.DeliveryDate = delivery.DeliveryDate;
		existing.Area = delivery.Area;
		existing.TotalWeightLoaded = delivery.TotalWeightLoaded;
		existing.RemainingWeight = delivery.RemainingWeight;
		existing.VehicleId = delivery.VehicleId;
		existing.DriverId = delivery.DriverId;
		//existing.FarmStockId = delivery.FarmStockId;

		//// Clear and add orders
		//existing.Orders.Clear();
		//foreach (var item in delivery.Orders)
		//{
		//	var newOrder = new Order()
		//	{
		//		Id = item.OrderId,
		//		QuantityInKg = item.QuantityInKg,
		//		RatePerKg = item.RatePerKg,
		//		TotalAmount = item.TotalAmount,
		//		PaidAmount = item.PaidAmount,
		//		BalanceAmount = item.BalanceAmount,
		//		ShopId = item.ShopId
		//	};
		//	existing.Orders.Add(newOrder);
		//}

		// Clear and add transport costs
		existing.TransportCosts.Clear();
		foreach (var t in delivery.TransportCosts)
		{
			var costData = new TransportCost()
			{
				Id = t.Id,
				VehicleId = t.VehicleId,
				ExpenseType = t.ExpenseType,
				Amount = t.Amount,
				Notes = t.Notes
			};
			existing.TransportCosts.Add(costData);
		}

		_unitOfWork.Deliverys.UpdateAsync(existing);
		return await _unitOfWork.Deliverys.SaveChangesAsync();
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var existing = await _unitOfWork.Deliverys.GetByIdAsync(id);
		if (existing == null) return false;

		_unitOfWork.Deliverys.DeleteAsync(existing);
		return await _unitOfWork.Deliverys.SaveChangesAsync();
	}

	public async Task<IEnumerable<DeliveryDto>> GetDeliveriesByDateAsync(DateTime date)
	{
		var all = await _unitOfWork.Deliverys.GetAllAsync();
		var rel = all.Where(d => d.DeliveryDate.Date == date.Date).ToList();
		return rel.Select(d => new DeliveryDto
		{
			DeliveryId = d.Id,
			DriverId = d.DriverId,
			VehicleId = d.VehicleId,
			DeliveryDate = d.DeliveryDate,
			TotalWeightLoaded = d.TotalWeightLoaded,
			//Orders = d.Orders.Select(o => new OrderDto
			//{
			//	OrderId = o.Id,
			//	OrderDate = o.OrderDate,
			//	QuantityInKg = o.QuantityInKg,
			//	ShopId = o.ShopId,
			//	RatePerKg = o.RatePerKg,
			//	PaidAmount = o.PaidAmount,
			//	BalanceAmount = o.BalanceAmount,
			//	TotalAmount = o.TotalAmount,
			//}).ToList(),
			TransportCosts = d.TransportCosts.Select(t => new TransportCost
			{
				Id = t.Id,
				VehicleId = t.VehicleId,
				ExpenseType = t.ExpenseType,
				Amount = t.Amount,
				Notes = t.Notes
			}).ToList()
		}).ToList();
	}

	public async Task<IEnumerable<DeliveryDto>> GetDeliveriesByDriverAsync(int driverId, DateTime? date = null)
	{
		var all = await _unitOfWork.Deliverys.GetAllAsync();
		var rel= all.Where(d =>
			d.DriverId == driverId &&
			(!date.HasValue || d.DeliveryDate.Date == date.Value.Date)).ToList();
		return rel.Select(d => new DeliveryDto
		{
			DeliveryId = d.Id,
			DriverId = d.DriverId,
			VehicleId = d.VehicleId,
			DeliveryDate = d.DeliveryDate,
			TotalWeightLoaded = d.TotalWeightLoaded,
			//Orders = d.Orders.Select(o => new OrderDto
			//{
			//	OrderId = o.Id,
			//	OrderDate = o.OrderDate,
			//	QuantityInKg = o.QuantityInKg,
			//	ShopId = o.ShopId,
			//	RatePerKg = o.RatePerKg,
			//	PaidAmount = o.PaidAmount,
			//	BalanceAmount = o.BalanceAmount,
			//	TotalAmount = o.TotalAmount,
			//}).ToList(),
			TransportCosts = d.TransportCosts.Select(t => new TransportCost
			{
				Id = t.Id,
				VehicleId = t.VehicleId,
				ExpenseType = t.ExpenseType,
				Amount = t.Amount,
				Notes = t.Notes
			}).ToList()
		}).ToList();
	}

	public async Task<IEnumerable<DeliveryDto>> GetDeliveriesByVehicleAsync(int vehicleId, DateTime? date = null)
	{
		var all = await _unitOfWork.Deliverys.GetAllAsync();
		var rel = all.Where(d =>
			d.VehicleId == vehicleId &&
			(!date.HasValue || d.DeliveryDate.Date == date.Value.Date)).ToList();
		return rel.Select(d => new DeliveryDto
		{
			DeliveryId = d.Id,
			DriverId = d.DriverId,
			VehicleId = d.VehicleId,
			DeliveryDate = d.DeliveryDate,
			TotalWeightLoaded = d.TotalWeightLoaded,
			//Orders = d.Orders.Select(o => new OrderDto
			//{
			//	OrderId = o.Id,
			//	OrderDate = o.OrderDate,
			//	QuantityInKg = o.QuantityInKg,
			//	ShopId = o.ShopId,
			//	RatePerKg = o.RatePerKg,
			//	PaidAmount = o.PaidAmount,
			//	BalanceAmount = o.BalanceAmount,
			//	TotalAmount = o.TotalAmount,
			//}).ToList(),
			TransportCosts = d.TransportCosts.Select(t => new TransportCost
			{
				Id = t.Id,
				VehicleId = t.VehicleId,
				ExpenseType = t.ExpenseType,
				Amount = t.Amount,
				Notes = t.Notes
			}).ToList()
		}).ToList();
	}

	public async Task<double> GetRemainingLoadInVehicleAsync(int deliveryId)
	{
		var delivery = await _unitOfWork.Deliverys.GetByIdAsync(deliveryId);
		if (delivery == null) return 0;

		var orders = await _unitOfWork.Orders.GetAllAsync();
		var relatedOrders = orders.Where(o => o.DeliveryId == deliveryId);
		double totalOrderLoad = relatedOrders.Sum(o => o.QuantityInKg);

		return delivery.TotalWeightLoaded - totalOrderLoad;
	}

	public async Task<bool> AssignOrderToDeliveryAsync(int deliveryId, Order order)
	{
		var delivery = await _unitOfWork.Deliverys.GetByIdAsync(deliveryId);
		if (delivery == null) return false;

		var remainingLoad = await GetRemainingLoadInVehicleAsync(deliveryId);
		if (order.QuantityInKg > remainingLoad) return false;

		order.DeliveryId = deliveryId;
		_unitOfWork.Orders.AddAsync(order);
		return await _unitOfWork.Orders.SaveChangesAsync();
	}
}
