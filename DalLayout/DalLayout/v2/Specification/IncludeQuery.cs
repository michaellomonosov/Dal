using System.Collections.Generic;
using System.Linq;

namespace DalLayout.v2.Specification
{
	public class IncludeQuery<TEntity, TPreviousProperty> : IIncludeQuery<TEntity, TPreviousProperty>
	{
		public Dictionary<IIncludeQuery, string> PathMap { get; }
		public IncludeVisitor Visitor { get; } = new IncludeVisitor();

		public IncludeQuery(Dictionary<IIncludeQuery, string> pathMap)
		{
			PathMap = pathMap;
		}

		public HashSet<string> Paths => PathMap.Select(x => x.Value).ToHashSet();
	}
}
