using System;
using System.Linq.Expressions;

namespace DalLayout.v2.Specification
{
	public abstract class BaseProjectedSpecification<TEntity, TProjectedType> : BaseSpecification<TEntity> , IProjectedSpecification<TEntity, TProjectedType>
		where TEntity : class
		where TProjectedType : class
	{
		protected BaseProjectedSpecification()
		{
		}
		protected BaseProjectedSpecification(Expression<Func<TEntity, bool>> criteria)
		{
			Criterias.Add(criteria);
		}
		public Expression<Func<TEntity, TProjectedType>> Select { get; private set; }
		public void SelectDto(Expression<Func<TEntity, TProjectedType>> select)
		{
			Select = select; 
		}
	}	
}
