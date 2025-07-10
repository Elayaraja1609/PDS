using Domain.Entities;

namespace Application.Interfaces;

public interface IGenericRepo<T> where T : BaseEntity
{
	Task<T?> GetByIdAsync(int id);
	Task<IReadOnlyList<T>> GetAllAsync();
	void AddAsync(T entity);
	void UpdateAsync(T entity);
	void DeleteAsync(T entity);
	Task<bool> SaveChangesAsync();
	bool IsExist(int id);
	Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
	Task<int> CountAsync(ISpecification<T> spec);

}
