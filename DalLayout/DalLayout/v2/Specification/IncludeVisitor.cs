using System.Linq.Expressions;

namespace DalLayout.v2.Specification
{
	public class IncludeVisitor : ExpressionVisitor
	{
		public string Path { get; private set; } = string.Empty;

		protected override Expression VisitMember(MemberExpression node)
		{
			Path = string.IsNullOrEmpty(Path) ? node.Member.Name : $"{node.Member.Name}.{Path}";

			return base.VisitMember(node);
		}
	}
}
