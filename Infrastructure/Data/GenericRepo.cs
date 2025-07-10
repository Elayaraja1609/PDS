using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepo<T>(AppDbContext context) : IGenericRepo<T> where T : BaseEntity
{
	public void AddAsync(T entity)
	{
		context.Set<T>().Add(entity);
	}

	public Task<int> CountAsync(ISpecification<T> spec)
	{
		throw new NotImplementedException();
	}

	public void DeleteAsync(T entity)
	{
		context.Set<T>().Remove(entity);
	}

	public async Task<IReadOnlyList<T>> GetAllAsync()
	{
		return await context.Set<T>().ToListAsync();
	}

	public Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
	{
		throw new NotImplementedException();
	}

	public async Task<T?> GetByIdAsync(int id)
	{
		return await context.Set<T>().FindAsync(id);
	}

	public bool IsExist(int id)
	{
		return context.Set<T>().Any(e => e.Id == id);
	}

	public async Task<bool> SaveChangesAsync()
	{
		return await context.SaveChangesAsync() > 0;
	}

	public void UpdateAsync(T entity)
	{
		context.Set<T>().Attach(entity);
		context.Entry(entity).State = EntityState.Modified;
	}
}
