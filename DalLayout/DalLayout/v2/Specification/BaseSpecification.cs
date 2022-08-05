using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DalLayout.v2.Specification
{
	public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
		where TEntity : class
	{
		protected BaseSpecification()
		{
		}
		protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
		{
			Criterias.Add(criteria);
		}
		public List<Expression<Func<TEntity, bool>>> Criterias { get; } = new List<Expression<Func<TEntity, bool>>>();
		public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
		public List<string> IncludeStrings { get; } = new List<string>();
		public List<string> SelectStrings { get; } = new List<string>();
		public List<string> CriteriaStrings { get; } = new List<string>();
		public Expression<Func<TEntity, object>> OrderBy { get; private set; }
		public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
		public Expression<Func<TEntity, object>> GroupBy { get; private set; }
		public int Take { get; private set; }
		public int Skip { get; private set; }
		public bool IsPagingEnabled { get; private set; }
		public bool AsNoTracking { get; private set; }

		protected virtual void AddCriteria(Expression<Func<TEntity, bool>> expression)
		{
			Criterias.Add(expression);
		}
		protected virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
		{
			Includes.Add(includeExpression);
		}
		protected virtual void AddIncludes<TProperty>(Func<IncludeAggregator<TEntity>, IIncludeQuery<TEntity, TProperty>> includeGenerator)
		{
			var includeQuery = includeGenerator(new IncludeAggregator<TEntity>());
			IncludeStrings.AddRange(includeQuery.Paths);
		}
		protected virtual void AddInclude(string includeString)
		{
			IncludeStrings.Add(includeString);
		}
		protected virtual void AddSelects<TProperty>(Func<IncludeAggregator<TEntity>, IIncludeQuery<TEntity, TProperty>> includeGenerator)
		{
			var includeQuery = includeGenerator(new IncludeAggregator<TEntity>());
			SelectStrings.AddRange(includeQuery.Paths);
		}
		protected virtual void AddSelect(string selectString)
		{
			SelectStrings.Add(selectString);

		}
		protected virtual void ApplyPaging(int skip, int take)
		{
			Skip = skip;
			Take = take;
			IsPagingEnabled = true;
		}
		protected virtual void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
		{
			OrderBy = orderByExpression;
		}
		protected virtual void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
		{
			OrderByDescending = orderByDescExpression;
		}
		protected virtual void ApplyGroupBy(Expression<Func<TEntity, object>> groupByExpression)
		{
			GroupBy = groupByExpression;
		}
		protected virtual void UseNoTracking()
		{
			AsNoTracking = true;
		}
	}
}
