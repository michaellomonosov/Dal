using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DalLayout.v2.Specification
{
	public static class SpecificationEvaluator
	{
		public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
			where TEntity : class
		{
			var query = inputQuery;
			if (spec.AsNoTracking)
			{
				query = query.AsNoTracking();
			}
			query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

			if (spec.Criterias != null && spec.Criterias.Any())
			{
				foreach (var specificationCondition in spec.Criterias)
				{
					query = query.Where(specificationCondition);
				}
			}
			if (spec.OrderBy != null)
			{
				query = query.OrderBy(spec.OrderBy);
			}
			if (spec.OrderByDescending != null)
			{
				query = query.OrderByDescending(spec.OrderByDescending);
			}
			return query;
		}
		public static IQueryable<TProjectedType> GetQuery<TEntity, TProjectedType>(IQueryable<TEntity> inputQuery, IProjectedSpecification<TEntity, TProjectedType> spec)
			where TEntity : class
			where TProjectedType : class
		{
			var query = GetQuery<TEntity>(inputQuery, spec);
			var querySelect = query.Select(spec.Select);
			return querySelect;
		}
	}
}
