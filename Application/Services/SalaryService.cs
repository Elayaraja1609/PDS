using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class SalaryService : ISalaryService
{
    private readonly IGenericRepo<Salary> _salaryRepo;
    private readonly IGenericRepo<Driver> _driverRepo;
    private readonly IGenericRepo<DailyWorkLog> _workLogRepo;

    public SalaryService(
        IGenericRepo<Salary> salaryRepo,
        IGenericRepo<Driver> driverRepo,
        IGenericRepo<DailyWorkLog> workLogRepo)
    {
        _salaryRepo = salaryRepo;
        _driverRepo = driverRepo;
        _workLogRepo = workLogRepo;
    }

    public async Task<SalaryDto> CreateAsync(CreateSalaryDto createDto)
    {
        var driver = await _driverRepo.GetByIdAsync(createDto.DriverId);
        if (driver == null) throw new ArgumentException("Driver not found");

        var daysWorked = await CalculateDaysWorkedAsync(createDto.DriverId, createDto.Month, createDto.Year);
        var totalSalary = (driver.DailyRate * daysWorked) + createDto.Bonus;

        var salary = new Salary
        {
            DriverId = createDto.DriverId,
            Month = createDto.Month,
            Year = createDto.Year,
            DaysWorked = daysWorked,
            BaseSalary = driver.BaseSalary,
            DailyRate = driver.DailyRate,
            Bonus = createDto.Bonus,
            TotalSalary = totalSalary,
            Notes = createDto.Notes
        };

        var result = await _salaryRepo.AddAsync(salary);
        return await MapToDto(result);
    }

    public async Task<SalaryDto> GetByIdAsync(int id)
    {
        var salary = await _salaryRepo.GetByIdAsync(id);
        if (salary == null) return null;
        return await MapToDto(salary);
    }

    public async Task<IEnumerable<SalaryDto>> GetAllAsync()
    {
        var salaries = await _salaryRepo.GetAllAsync();
        var dtos = new List<SalaryDto>();
        
        foreach (var salary in salaries)
        {
            dtos.Add(await MapToDto(salary));
        }
        
        return dtos;
    }

    public async Task<IEnumerable<SalaryDto>> GetByDriverAsync(int driverId)
    {
        var salaries = await _salaryRepo.GetAsync(x => x.DriverId == driverId);
        var dtos = new List<SalaryDto>();
        
        foreach (var salary in salaries)
        {
            dtos.Add(await MapToDto(salary));
        }
        
        return dtos;
    }

    public async Task<IEnumerable<SalaryDto>> GetByMonthAsync(int month, int year)
    {
        var salaries = await _salaryRepo.GetAsync(x => x.Month == month && x.Year == year);
        var dtos = new List<SalaryDto>();
        
        foreach (var salary in salaries)
        {
            dtos.Add(await MapToDto(salary));
        }
        
        return dtos;
    }

    public async Task<SalaryDto> UpdateAsync(int id, CreateSalaryDto updateDto)
    {
        var salary = await _salaryRepo.GetByIdAsync(id);
        if (salary == null) return null;

        var driver = await _driverRepo.GetByIdAsync(updateDto.DriverId);
        if (driver == null) throw new ArgumentException("Driver not found");

        var daysWorked = await CalculateDaysWorkedAsync(updateDto.DriverId, updateDto.Month, updateDto.Year);
        var totalSalary = (driver.DailyRate * daysWorked) + updateDto.Bonus;

        salary.DriverId = updateDto.DriverId;
        salary.Month = updateDto.Month;
        salary.Year = updateDto.Year;
        salary.DaysWorked = daysWorked;
        salary.BaseSalary = driver.BaseSalary;
        salary.DailyRate = driver.DailyRate;
        salary.Bonus = updateDto.Bonus;
        salary.TotalSalary = totalSalary;
        salary.Notes = updateDto.Notes;

        var result = await _salaryRepo.UpdateAsync(salary);
        return await MapToDto(result);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _salaryRepo.DeleteAsync(id);
    }

    public async Task<bool> MarkAsPaidAsync(int id, string paymentMethod)
    {
        var salary = await _salaryRepo.GetByIdAsync(id);
        if (salary == null) return false;

        salary.IsPaid = true;
        salary.PaymentDate = DateTime.UtcNow;
        salary.PaymentMethod = paymentMethod;

        await _salaryRepo.UpdateAsync(salary);
        return true;
    }

    public async Task<PayrollSummaryDto> GetPayrollSummaryAsync(int month, int year)
    {
        var salaries = await GetByMonthAsync(month, year);
        var salaryList = salaries.ToList();

        return new PayrollSummaryDto
        {
            Month = month,
            Year = year,
            TotalStaff = salaryList.Count,
            TotalSalary = salaryList.Sum(x => x.TotalSalary),
            TotalBonus = salaryList.Sum(x => x.Bonus),
            TotalPaid = salaryList.Where(x => x.IsPaid).Sum(x => x.TotalSalary),
            TotalPending = salaryList.Where(x => !x.IsPaid).Sum(x => x.TotalSalary),
            Salaries = salaryList
        };
    }

    public async Task<bool> GenerateMonthlySalariesAsync(int month, int year)
    {
        var drivers = await _driverRepo.GetAsync(x => x.IsActive);
        var existingSalaries = await _salaryRepo.GetAsync(x => x.Month == month && x.Year == year);
        var existingDriverIds = existingSalaries.Select(x => x.DriverId).ToList();

        foreach (var driver in drivers)
        {
            if (!existingDriverIds.Contains(driver.Id))
            {
                var createDto = new CreateSalaryDto
                {
                    DriverId = driver.Id,
                    Month = month,
                    Year = year,
                    Bonus = 0
                };

                await CreateAsync(createDto);
            }
        }

        return true;
    }

    public async Task<double> CalculateSalaryAsync(int driverId, int month, int year)
    {
        var driver = await _driverRepo.GetByIdAsync(driverId);
        if (driver == null) return 0;

        var daysWorked = await CalculateDaysWorkedAsync(driverId, month, year);
        return driver.DailyRate * daysWorked;
    }

    private async Task<int> CalculateDaysWorkedAsync(int driverId, int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        
        var workLogs = await _workLogRepo.GetAsync(x => 
            x.DriverId == driverId && 
            x.WorkDate >= startDate && 
            x.WorkDate <= endDate &&
            x.IsPresent);
            
        return workLogs.Count();
    }

    private async Task<SalaryDto> MapToDto(Salary salary)
    {
        var driver = await _driverRepo.GetByIdAsync(salary.DriverId);

        return new SalaryDto
        {
            Id = salary.Id,
            DriverId = salary.DriverId,
            DriverName = driver?.Name ?? "Unknown",
            Month = salary.Month,
            Year = salary.Year,
            DaysWorked = salary.DaysWorked,
            BaseSalary = salary.BaseSalary,
            DailyRate = salary.DailyRate,
            Bonus = salary.Bonus,
            TotalSalary = salary.TotalSalary,
            IsPaid = salary.IsPaid,
            PaymentDate = salary.PaymentDate,
            PaymentMethod = salary.PaymentMethod,
            Notes = salary.Notes,
            CreatedAt = salary.CreatedAt
        };
    }
}
