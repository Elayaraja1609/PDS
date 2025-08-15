using Application.DTOs.ResponsesDto.cs;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Data;

public class UnitOfWork: IUnitOfWork
{
	private readonly AppDbContext _context;
	private IGenericRepo<User>? _users;
	private IGenericRepo<Farm>? _farms;
	private IGenericRepo<ChickenBatch>? _chickenBatchs;
	private IGenericRepo<FarmStock>? _farmStocks;
	private IGenericRepo<Vehicle>? _vehicles;
	private IGenericRepo<Driver>? _drivers;
	private IGenericRepo<Delivery>? _deliverys;
	private IGenericRepo<Shop>? _shops;
	private IGenericRepo<Order>? _orders;
	private IGenericRepo<Payment>? _payments;
	private IGenericRepo<TransportCost>? _transportCosts;
	private IGenericRepo<MiscExpense>? _miscExpenses;
	private IGenericRepo<DeliveryDetail>? _deliveryDetail;
	private IGenericRepo<DeliveryUpdatesResDto>? _deliveryUpdatesRes;
	
	// New repositories
	private IGenericRepo<DailyWorkLog>? _dailyWorkLogs;
	private IGenericRepo<Expense>? _expenses;
	private IGenericRepo<Salary>? _salaries;
	
	public UnitOfWork(AppDbContext context)
	{
		_context = context;
	}
	public IGenericRepo<User> AppUsers => _users ??= new GenericRepo<User>(_context);
	public IGenericRepo<Farm> Farms => _farms ??= new GenericRepo<Farm>(_context);
	public IGenericRepo<ChickenBatch> ChickenBatchs => _chickenBatchs ??= new GenericRepo<ChickenBatch>(_context);
	public IGenericRepo<FarmStock> FarmStocks => _farmStocks ??= new GenericRepo<FarmStock>(_context);
	public IGenericRepo<Vehicle> Vehicles => _vehicles ??= new GenericRepo<Vehicle>(_context);
	public IGenericRepo<Driver> Drivers => _drivers ??= new GenericRepo<Driver>(_context);
	public IGenericRepo<Delivery> Deliverys => _deliverys ??= new GenericRepo<Delivery>(_context);
	public IGenericRepo<Shop> Shops => _shops ??= new GenericRepo<Shop>(_context);
	public IGenericRepo<Order> Orders => _orders ??= new GenericRepo<Order>(_context);
	public IGenericRepo<Payment> Payments => _payments ??= new GenericRepo<Payment>(_context);
	public IGenericRepo<TransportCost> TransportCosts => _transportCosts ??= new GenericRepo<TransportCost>(_context);
	public IGenericRepo<MiscExpense> MiscExpenses => _miscExpenses ??= new GenericRepo<MiscExpense>(_context);
	public IGenericRepo<DeliveryDetail> DeliveryDetails => _deliveryDetail ??= new GenericRepo<DeliveryDetail>(_context);
	public IGenericRepo<DeliveryUpdatesResDto> DeliveryUpdatesRes => _deliveryUpdatesRes ??= new GenericRepo<DeliveryUpdatesResDto>(_context);
	
	// New repository properties
	public IGenericRepo<DailyWorkLog> DailyWorkLogs => _dailyWorkLogs ??= new GenericRepo<DailyWorkLog>(_context);
	public IGenericRepo<Expense> Expenses => _expenses ??= new GenericRepo<Expense>(_context);
	public IGenericRepo<Salary> Salaries => _salaries ??= new GenericRepo<Salary>(_context);
	
	public async Task<int> CompleteAsync()
	{
		return await _context.SaveChangesAsync();
	}
	public void Dispose()
	{
		_context.Dispose();
	}
}
