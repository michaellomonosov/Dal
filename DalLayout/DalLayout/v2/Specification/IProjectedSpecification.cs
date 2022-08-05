using System;
using System.Linq.Expressions;

namespace DalLayout.v2.Specification
{
	public interface IProjectedSpecification<TEntity, TProjectedType> : ISpecification<TEntity>
		where TEntity : class
		where TProjectedType : class
	{
		Expression<Func<TEntity, TProjectedType>> Select { get; }
	}
}
