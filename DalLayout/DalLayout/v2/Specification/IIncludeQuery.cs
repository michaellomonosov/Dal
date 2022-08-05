using System.Collections.Generic;

namespace DalLayout.v2.Specification
{
	public interface IIncludeQuery
	{
		Dictionary<IIncludeQuery, string> PathMap { get; }
		IncludeVisitor Visitor { get; }
		HashSet<string> Paths { get; }
	}
	public interface IIncludeQuery<TEntity, out TPreviousProperty> : IIncludeQuery
	{
	}
}
