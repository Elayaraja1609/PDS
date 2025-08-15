using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IGenericRepo<Expense> _expenseRepo;
    private readonly IGenericRepo<Vehicle> _vehicleRepo;
    private readonly IGenericRepo<Driver> _driverRepo;

    public ExpenseService(
        IGenericRepo<Expense> expenseRepo,
        IGenericRepo<Vehicle> vehicleRepo,
        IGenericRepo<Driver> driverRepo)
    {
        _expenseRepo = expenseRepo;
        _vehicleRepo = vehicleRepo;
        _driverRepo = driverRepo;
    }

    public async Task<ExpenseDto> CreateAsync(CreateExpenseDto createDto)
    {
        var expense = new Expense
        {
            ExpenseDate = createDto.ExpenseDate,
            Description = createDto.Description,
            Amount = createDto.Amount,
            Category = createDto.Category,
            VehicleId = createDto.VehicleId,
            DriverId = createDto.DriverId,
            ReceiptNumber = createDto.ReceiptNumber,
            Notes = createDto.Notes,
            CreatedBy = "System" // This should come from the authenticated user
        };

        var result = await _expenseRepo.AddAsync(expense);
        return await MapToDto(result);
    }

    public async Task<ExpenseDto> GetByIdAsync(int id)
    {
        var expense = await _expenseRepo.GetByIdAsync(id);
        if (expense == null) return null;
        return await MapToDto(expense);
    }

    public async Task<IEnumerable<ExpenseDto>> GetAllAsync()
    {
        var expenses = await _expenseRepo.GetAllAsync();
        var dtos = new List<ExpenseDto>();
        
        foreach (var expense in expenses)
        {
            dtos.Add(await MapToDto(expense));
        }
        
        return dtos;
    }

    public async Task<IEnumerable<ExpenseDto>> GetByCategoryAsync(string category)
    {
        var expenses = await _expenseRepo.GetAsync(x => x.Category == category);
        var dtos = new List<ExpenseDto>();
        
        foreach (var expense in expenses)
        {
            dtos.Add(await MapToDto(expense));
        }
        
        return dtos;
    }

    public async Task<IEnumerable<ExpenseDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var expenses = await _expenseRepo.GetAsync(x => x.ExpenseDate >= startDate && x.ExpenseDate <= endDate);
        var dtos = new List<ExpenseDto>();
        
        foreach (var expense in expenses)
        {
            dtos.Add(await MapToDto(expense));
        }
        
        return dtos;
    }

    public async Task<IEnumerable<ExpenseDto>> GetByVehicleAsync(int vehicleId)
    {
        var expenses = await _expenseRepo.GetAsync(x => x.VehicleId == vehicleId);
        var dtos = new List<ExpenseDto>();
        
        foreach (var expense in expenses)
        {
            dtos.Add(await MapToDto(expense));
        }
        
        return dtos;
    }

    public async Task<IEnumerable<ExpenseDto>> GetByDriverAsync(int driverId)
    {
        var expenses = await _expenseRepo.GetAsync(x => x.DriverId == driverId);
        var dtos = new List<ExpenseDto>();
        
        foreach (var expense in expenses)
        {
            dtos.Add(await MapToDto(expense));
        }
        
        return dtos;
    }

    public async Task<ExpenseDto> UpdateAsync(int id, CreateExpenseDto updateDto)
    {
        var expense = await _expenseRepo.GetByIdAsync(id);
        if (expense == null) return null;

        expense.ExpenseDate = updateDto.ExpenseDate;
        expense.Description = updateDto.Description;
        expense.Amount = updateDto.Amount;
        expense.Category = updateDto.Category;
        expense.VehicleId = updateDto.VehicleId;
        expense.DriverId = updateDto.DriverId;
        expense.ReceiptNumber = updateDto.ReceiptNumber;
        expense.Notes = updateDto.Notes;

        var result = await _expenseRepo.UpdateAsync(expense);
        return await MapToDto(result);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _expenseRepo.DeleteAsync(id);
    }

    public async Task<double> GetTotalExpensesByMonthAsync(int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        
        var expenses = await _expenseRepo.GetAsync(x => 
            x.ExpenseDate >= startDate && 
            x.ExpenseDate <= endDate);
            
        return expenses.Sum(x => x.Amount);
    }

    public async Task<Dictionary<string, double>> GetExpenseBreakdownByMonthAsync(int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        
        var expenses = await _expenseRepo.GetAsync(x => 
            x.ExpenseDate >= startDate && 
            x.ExpenseDate <= endDate);
            
        return expenses
            .GroupBy(x => x.Category)
            .ToDictionary(g => g.Key, g => g.Sum(x => x.Amount));
    }

    private async Task<ExpenseDto> MapToDto(Expense expense)
    {
        var vehicle = expense.VehicleId.HasValue ? await _vehicleRepo.GetByIdAsync(expense.VehicleId.Value) : null;
        var driver = expense.DriverId.HasValue ? await _driverRepo.GetByIdAsync(expense.DriverId.Value) : null;

        return new ExpenseDto
        {
            Id = expense.Id,
            ExpenseDate = expense.ExpenseDate,
            Description = expense.Description,
            Amount = expense.Amount,
            Category = expense.Category,
            VehicleId = expense.VehicleId,
            VehicleNumber = vehicle?.VehicleNumber,
            DriverId = expense.DriverId,
            DriverName = driver?.Name,
            ReceiptNumber = expense.ReceiptNumber,
            Notes = expense.Notes,
            CreatedAt = expense.CreatedAt,
            CreatedBy = expense.CreatedBy
        };
    }
}
