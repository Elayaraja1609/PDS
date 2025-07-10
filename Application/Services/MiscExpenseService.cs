using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;
public class MiscExpenseService : IMiscExpenseService
{
	private readonly IUnitOfWork _unitOfWork;

	public MiscExpenseService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<MiscExpense>> GetAllAsync()
	{
		return await _unitOfWork.MiscExpenses.GetAllAsync();
	}

	public async Task<MiscExpense?> GetByIdAsync(int id)
	{
		return await _unitOfWork.MiscExpenses.GetByIdAsync(id);
	}

	public async Task<MiscExpense> CreateAsync(MiscExpense expense)
	{
		_unitOfWork.MiscExpenses.AddAsync(expense);
		await _unitOfWork.MiscExpenses.SaveChangesAsync();
		return expense;
	}

	public async Task<bool> UpdateAsync(MiscExpense expense)
	{
		_unitOfWork.MiscExpenses.UpdateAsync(expense);
		return await _unitOfWork.MiscExpenses.SaveChangesAsync();
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var expense = await _unitOfWork.MiscExpenses.GetByIdAsync(id);
		if (expense == null) return false;

		_unitOfWork.MiscExpenses.DeleteAsync(expense);
		return await _unitOfWork.MiscExpenses.SaveChangesAsync();
	}

	public async Task<IEnumerable<MiscExpense>> GetByDateAsync(DateTime date)
	{
		var all = await _unitOfWork.MiscExpenses.GetAllAsync();
		return all.Where(e => e.Date.Date == date.Date).ToList();
	}

	public async Task<decimal> GetTotalExpensesByDateAsync(DateTime date)
	{
		var expenses = await GetByDateAsync(date);
		return expenses.Sum(e => e.Amount);
	}
}
