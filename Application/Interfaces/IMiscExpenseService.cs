using Domain.Entities;

namespace Application.Interfaces;
public interface IMiscExpenseService
{
	Task<IEnumerable<MiscExpense>> GetAllAsync();
	Task<MiscExpense?> GetByIdAsync(int id);
	Task<MiscExpense> CreateAsync(MiscExpense expense);
	Task<bool> UpdateAsync(MiscExpense expense);
	Task<bool> DeleteAsync(int id);

	Task<IEnumerable<MiscExpense>> GetByDateAsync(DateTime date);
	Task<decimal> GetTotalExpensesByDateAsync(DateTime date);
}

