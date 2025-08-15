using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class DailyWorkLogService : IDailyWorkLogService
{
    private readonly IGenericRepo<DailyWorkLog> _workLogRepo;
    private readonly IGenericRepo<Driver> _driverRepo;
    private readonly IGenericRepo<Vehicle> _vehicleRepo;

    public DailyWorkLogService(
        IGenericRepo<DailyWorkLog> workLogRepo,
        IGenericRepo<Driver> driverRepo,
        IGenericRepo<Vehicle> vehicleRepo)
    {
        _workLogRepo = workLogRepo;
        _driverRepo = driverRepo;
        _vehicleRepo = vehicleRepo;
    }

    public async Task<DailyWorkLogDto> CreateAsync(CreateDailyWorkLogDto createDto)
    {
        var workLog = new DailyWorkLog
        {
            WorkDate = createDto.WorkDate,
            DriverId = createDto.DriverId,
            AssistantId = createDto.AssistantId,
            VehicleId = createDto.VehicleId,
            Route = createDto.Route,
            IsPresent = createDto.IsPresent,
            StartTime = createDto.StartTime,
            EndTime = createDto.EndTime,
            FuelExpense = createDto.FuelExpense,
            TollCharges = createDto.TollCharges,
            DailyAllowance = createDto.DailyAllowance,
            Notes = createDto.Notes
        };

        var result = await _workLogRepo.AddAsync(workLog);
        return await MapToDto(result);
    }

    public async Task<DailyWorkLogDto> GetByIdAsync(int id)
    {
        var workLog = await _workLogRepo.GetByIdAsync(id);
        if (workLog == null) return null;
        return await MapToDto(workLog);
    }

    public async Task<IEnumerable<DailyWorkLogDto>> GetAllAsync()
    {
        var workLogs = await _workLogRepo.GetAllAsync();
        var dtos = new List<DailyWorkLogDto>();
        
        foreach (var workLog in workLogs)
        {
            dtos.Add(await MapToDto(workLog));
        }
        
        return dtos;
    }

    public async Task<IEnumerable<DailyWorkLogDto>> GetByDriverAsync(int driverId)
    {
        var workLogs = await _workLogRepo.GetAsync(x => x.DriverId == driverId);
        var dtos = new List<DailyWorkLogDto>();
        
        foreach (var workLog in workLogs)
        {
            dtos.Add(await MapToDto(workLog));
        }
        
        return dtos;
    }

    public async Task<IEnumerable<DailyWorkLogDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var workLogs = await _workLogRepo.GetAsync(x => x.WorkDate >= startDate && x.WorkDate <= endDate);
        var dtos = new List<DailyWorkLogDto>();
        
        foreach (var workLog in workLogs)
        {
            dtos.Add(await MapToDto(workLog));
        }
        
        return dtos;
    }

    public async Task<DailyWorkLogDto> UpdateAsync(int id, CreateDailyWorkLogDto updateDto)
    {
        var workLog = await _workLogRepo.GetByIdAsync(id);
        if (workLog == null) return null;

        workLog.WorkDate = updateDto.WorkDate;
        workLog.DriverId = updateDto.DriverId;
        workLog.AssistantId = updateDto.AssistantId;
        workLog.VehicleId = updateDto.VehicleId;
        workLog.Route = updateDto.Route;
        workLog.IsPresent = updateDto.IsPresent;
        workLog.StartTime = updateDto.StartTime;
        workLog.EndTime = updateDto.EndTime;
        workLog.FuelExpense = updateDto.FuelExpense;
        workLog.TollCharges = updateDto.TollCharges;
        workLog.DailyAllowance = updateDto.DailyAllowance;
        workLog.Notes = updateDto.Notes;

        var result = await _workLogRepo.UpdateAsync(workLog);
        return await MapToDto(result);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _workLogRepo.DeleteAsync(id);
    }

    public async Task<IEnumerable<DailyWorkLogDto>> GetMonthlyWorkLogAsync(int driverId, int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        
        var workLogs = await _workLogRepo.GetAsync(x => 
            x.DriverId == driverId && 
            x.WorkDate >= startDate && 
            x.WorkDate <= endDate);
            
        var dtos = new List<DailyWorkLogDto>();
        
        foreach (var workLog in workLogs)
        {
            dtos.Add(await MapToDto(workLog));
        }
        
        return dtos;
    }

    private async Task<DailyWorkLogDto> MapToDto(DailyWorkLog workLog)
    {
        var driver = await _driverRepo.GetByIdAsync(workLog.DriverId);
        var assistant = workLog.AssistantId.HasValue ? await _driverRepo.GetByIdAsync(workLog.AssistantId.Value) : null;
        var vehicle = await _vehicleRepo.GetByIdAsync(workLog.VehicleId);

        return new DailyWorkLogDto
        {
            Id = workLog.Id,
            WorkDate = workLog.WorkDate,
            DriverId = workLog.DriverId,
            DriverName = driver?.Name ?? "Unknown",
            AssistantId = workLog.AssistantId,
            AssistantName = assistant?.Name,
            VehicleId = workLog.VehicleId,
            VehicleNumber = vehicle?.VehicleNumber ?? "Unknown",
            Route = workLog.Route,
            IsPresent = workLog.IsPresent,
            StartTime = workLog.StartTime,
            EndTime = workLog.EndTime,
            FuelExpense = workLog.FuelExpense,
            TollCharges = workLog.TollCharges,
            DailyAllowance = workLog.DailyAllowance,
            Notes = workLog.Notes,
            CreatedAt = workLog.CreatedAt
        };
    }
}
