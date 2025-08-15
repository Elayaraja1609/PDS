using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces;

public interface IGenericRepo<T> where T : class
{
	Task<T?> GetByIdAsync(int id);
	Task<IReadOnlyList<T>> GetAllAsync();
	Task<T> AddAsync(T entity);
	Task<T> UpdateAsync(T entity);
	Task<bool> DeleteAsync(T entity);
	Task<bool> DeleteAsync(int id);
	Task<bool> SaveChangesAsync();
	//bool IsExist(int id);
	Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
	Task<int> CountAsync(ISpecification<T> spec);
	Task<List<TResult>> ExecuteStoredProcedureAsync<TResult>(string storedProcName, object parameterObject = null) where TResult : class;
	
	// New method for filtering entities
	Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
}
