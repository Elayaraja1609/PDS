using System.Linq.Expressions;

namespace Application.Interfaces;

public interface ISpecification<T>
{
	Expression<Func<T, bool>> Criteria { get; }
	List<Expression<Func<T, object>>> Includes { get; }
	Expression<Func<T, object>>? OrderBy { get; }
	Expression<Func<T, object>>? OrderByDescending { get; }
	bool IsDistinct { get; }
	int Take { get; }
	int Skip { get; }
	bool IsPagingEnabled { get; }
	//IQueryable<T> ApplyPaging(IQueryable<T> query);
}

public interface ISpecification<T, TKey> : ISpecification<T>
{
	Expression<Func<T, TKey>>? Select { get; }
}
