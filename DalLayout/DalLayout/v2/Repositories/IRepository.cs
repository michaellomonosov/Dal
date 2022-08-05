using DalLayout.v2.Specification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DalLayout.v2.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<IReadOnlyList<TEntity>> ListAllAsync();
		Task<int> AddAsync(TEntity entity);
		Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
		Task<int> UpdateAsync(TEntity entity);
		Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);
		Task<int> DeleteAsync(TEntity entity);
		Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities);
		Task<IReadOnlyList<TEntity>> GetListAsync(ISpecification<TEntity> specification);
		Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification);
		Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> specification);
		Task<long> CountAsync(ISpecification<TEntity> specification);
		Task<bool> AnyAsync(ISpecification<TEntity> specification);

		Task<TProjectedType> FirstOrDefaultAsync<TProjectedType>(IProjectedSpecification<TEntity, TProjectedType> specification) where TProjectedType: class;
		Task<TProjectedType> SingleOrDefaultAsync<TProjectedType>(IProjectedSpecification<TEntity, TProjectedType> specification) where TProjectedType : class;
		Task<IReadOnlyList<TProjectedType>> GetListAsync<TProjectedType>(IProjectedSpecification<TEntity, TProjectedType> specification) where TProjectedType : class;

		Task<TReturnEntity> FirstOrDefaultAsync<TReturnEntity>(IRawSqlSpecification<TReturnEntity> rawSqlSpecification);
		Task<TReturnEntity> SingleOrDefaultAsync<TReturnEntity>(IRawSqlSpecification<TReturnEntity> rawSqlSpecification);
		Task<IEnumerable<TEnity>> GetListAsync<TEnity>(IRawSqlSpecification<TEntity> rawSqlSpecification);
		Task<int> CountAsync(IRawSqlSpecification rawSqlSpecification);
		Task<int> Exec(IRawSqlSpecification rawSqlSpecification);

	}
}