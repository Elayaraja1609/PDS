using Application.DTOs.ResponsesDto.cs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUnitOfWork: IDisposable
{
	IGenericRepo<User> AppUsers { get; }
	IGenericRepo<Farm> Farms { get; }
	IGenericRepo<ChickenBatch> ChickenBatchs { get; }
	IGenericRepo<FarmStock> FarmStocks { get; }
	IGenericRepo<Vehicle>Vehicles { get; }
	IGenericRepo<Driver>Drivers { get; }
	IGenericRepo<Delivery>Deliverys { get; }
	IGenericRepo<Shop>Shops { get; }
	IGenericRepo<Order>Orders { get; }
	IGenericRepo<Payment>Payments { get; }
	IGenericRepo<TransportCost>TransportCosts { get; }
	IGenericRepo<MiscExpense>MiscExpenses { get; }
	IGenericRepo<DeliveryDetail> DeliveryDetails { get; }
	IGenericRepo<DeliveryUpdatesResDto> DeliveryUpdatesRes { get; }
	Task<int> CompleteAsync();
}
