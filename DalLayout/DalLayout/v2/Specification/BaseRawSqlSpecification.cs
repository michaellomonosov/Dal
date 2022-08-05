namespace DalLayout.v2.Specification
{
	public abstract class BaseRawSqlSpecification<TEntity> : IRawSqlSpecification<TEntity>
	{
		protected BaseRawSqlSpecification()
		{
		}
		public string RawSql { get; private set; }
		public object Params { get; private set; }
	}
}
