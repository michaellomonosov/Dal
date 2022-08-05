namespace DalLayout.v2.Specification
{
	public interface IRawSqlSpecification
	{
		string RawSql { get; }
		object Params { get; }
	}
	public interface IRawSqlSpecification<TEntity> : IRawSqlSpecification
	{
	}
}
