using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
	private readonly AppDbContext _context;

	public GenericRepo(AppDbContext context)
	{
		_context = context;
	}
	public async Task<List<T>> ExecuteStoredProcedureAsync<T>(string storedProcName, object parameterObject = null) where T : class
	{
		var sqlParams = new List<SqlParameter>();

		if (parameterObject != null)
		{
			var props = parameterObject.GetType().GetProperties();

			foreach (var prop in props)
			{
				var name = $"@{prop.Name}";
				var value = prop.GetValue(parameterObject) ?? DBNull.Value;

				sqlParams.Add(new SqlParameter(name, value));
			}
		}

		// Build SQL parameter placeholders
		string sqlCommand = $"EXEC {storedProcName}";

		if (sqlParams.Any())
		{
			sqlCommand += " " + string.Join(", ", sqlParams.Select(p => $"{p.ParameterName} = {FormatSqlValue(p.Value)}"));
		}

		var rel= await _context.Set<T>()
			.FromSqlRaw(sqlCommand)
			.ToListAsync();
		return rel;
	}
	private string FormatSqlValue(object value)
	{
		if (value == null || value == DBNull.Value)
			return "NULL";

		if (value is string)
			return $"'{value}'";

		if (value is DateTime dt)
			return $"'{dt:yyyy-MM-dd HH:mm:ss}'";  // Ensure SQL-friendly format

		if (value is bool b)
			return b ? "1" : "0";

		return value.ToString();
	}

	public async Task<T> AddAsync(T entity)
	{
		_context.Set<T>().Add(entity);
		return entity;
	}

	public Task<int> CountAsync(ISpecification<T> spec)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> DeleteAsync(T entity)
	{
		_context.Set<T>().Remove(entity);
		return true;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var entity = await GetByIdAsync(id);
		if (entity == null) return false;
		_context.Set<T>().Remove(entity);
		return true;
	}

	public async Task<IReadOnlyList<T>> GetAllAsync()
	{
		return await _context.Set<T>().ToListAsync();
	}

	public Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
	{
		throw new NotImplementedException();
	}

	public async Task<T?> GetByIdAsync(int id)
	{
		return await _context.Set<T>().FindAsync(id);
	}

	//public bool IsExist(int id)
	//{
	//	return _context.Set<T>().Any(e => e.Id == id);
	//}

	public async Task<bool> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public async Task<T> UpdateAsync(T entity)
	{
		_context.Set<T>().Attach(entity);
		_context.Entry(entity).State = EntityState.Modified;
		return entity;
	}

	// New method for filtering entities
	public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
	{
		return await _context.Set<T>().Where(predicate).ToListAsync();
	}
	
}

