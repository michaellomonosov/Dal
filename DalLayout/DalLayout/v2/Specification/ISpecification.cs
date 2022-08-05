using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DalLayout.v2.Specification
{
	public interface ISpecification<TEntity>
		where TEntity : class
	{
		List<Expression<Func<TEntity, bool>>> Criterias { get; }
		List<Expression<Func<TEntity, object>>> Includes { get; }
		Expression<Func<TEntity, object>> OrderBy { get; }
		Expression<Func<TEntity, object>> OrderByDescending { get; }
		Expression<Func<TEntity, object>> GroupBy { get; }
		List<string> IncludeStrings { get; }
		List<string> SelectStrings { get; }
		List<string> CriteriaStrings { get; }
		int Take { get; }
		int Skip { get; }
		bool IsPagingEnabled { get; }
		bool AsNoTracking { get; }
	}
}
