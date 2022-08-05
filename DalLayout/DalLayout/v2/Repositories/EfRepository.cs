using Dapper;
using Microsoft.EntityFrameworkCore;
using DalLayout.v2.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DalLayout.v2.Repositories
{
	/// <summary>
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TContext"></typeparam>
	public abstract class EfRepository<TEntity, TContext> : IEfRepository<TEntity, TContext>
		where TEntity : class
		where TContext : DbContext
	{
		/// <summary>
		/// </summary>
		protected readonly TContext _сontext;
		protected DbSet<TEntity> _entities;
		/// <summary>
		/// </summary>
		/// <param name="dbContext"></param>
		protected EfRepository(TContext dbContext)
		{
			_сontext = dbContext;
			_entities = _сontext.Set<TEntity>();
		}
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public virtual async Task<IReadOnlyList<TEntity>> ListAllAsync()
		{
			return await _entities.ToListAsync();
		}
		/// <summary>
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual async Task<int> AddAsync(TEntity entity)
		{
			_сontext.ChangeTracker.AutoDetectChangesEnabled = false;
			await _entities.AddAsync(entity);
			var result = await _сontext.SaveChangesAsync();
			_сontext.ChangeTracker.AutoDetectChangesEnabled = true;
			return result;
		}

		public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
		{
			_сontext.ChangeTracker.AutoDetectChangesEnabled = false;
			await _entities.AddRangeAsync(entities);
			var result = await _сontext.SaveChangesAsync();
			_сontext.ChangeTracker.AutoDetectChangesEnabled = true;
			return result;
		}

		/// <summary>
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual async Task<int> UpdateAsync(TEntity entity)
		{
			_entities.Update(entity);
			return await _сontext.SaveChangesAsync();
		}

		public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
		{
			_entities.UpdateRange(entities);
			return await _сontext.SaveChangesAsync();
		}

		/// <summary>
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual async Task<int> DeleteAsync(TEntity entity)
		{
			_entities.Remove(entity);
			return await _сontext.SaveChangesAsync();
		}

		public virtual async Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities)
		{
			_entities.RemoveRange(entities);
			return await _сontext.SaveChangesAsync();
		}
		public async Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification)
		{
			return await ApplySpec(specification).FirstOrDefaultAsync();
		}
		public async Task<TProjectedType> FirstOrDefaultAsync<TProjectedType>(IProjectedSpecification<TEntity, TProjectedType> specification)
			where TProjectedType : class
		{
			return await ApplySpec(specification).FirstOrDefaultAsync();
		}
		public async Task<TReturnEntity> FirstOrDefaultAsync<TReturnEntity>(IRawSqlSpecification<TReturnEntity> rawSqlSpecification)
		{
			return await _сontext.Database.GetDbConnection().QueryFirstOrDefaultAsync<TReturnEntity>(rawSqlSpecification.RawSql, rawSqlSpecification.Params);
		}
		public async Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> specification)
		{
			return await ApplySpec(specification).SingleOrDefaultAsync();
		}
		public async Task<TProjectedType> SingleOrDefaultAsync<TProjectedType>(IProjectedSpecification<TEntity, TProjectedType> specification)
			where TProjectedType : class
		{
			return await ApplySpec(specification).SingleOrDefaultAsync();
		}
		public async Task<TReturnEntity> SingleOrDefaultAsync<TReturnEntity>(IRawSqlSpecification<TReturnEntity> rawSqlSpecification)
		{
			return await _сontext.Database.GetDbConnection().QuerySingleOrDefaultAsync<TReturnEntity>(rawSqlSpecification.RawSql, rawSqlSpecification.Params);
		}
		public async Task<bool> AnyAsync(ISpecification<TEntity> specification)
		{
			return await ApplySpec(specification).AnyAsync();
		}
		public async Task<IReadOnlyList<TEntity>> GetListAsync(ISpecification<TEntity> specification)
		{
			return await ApplySpec(specification).ToListAsync();
		}
		public async Task<IReadOnlyList<TProjectedType>> GetListAsync<TProjectedType>(IProjectedSpecification<TEntity, TProjectedType> specification)
			where TProjectedType : class
		{
			return await ApplySpec(specification).ToListAsync();
		}
		public async Task<IEnumerable<TEnity>> GetListAsync<TEnity>(IRawSqlSpecification<TEntity> rawSqlSpecification)
		{
			return await _сontext.Database.GetDbConnection().QueryAsync<TEnity>(rawSqlSpecification.RawSql, rawSqlSpecification.Params);
		}
		public async Task<long> CountAsync(ISpecification<TEntity> specification)
		{
			return await ApplySpec(specification).CountAsync();
		}
		public async Task<int> CountAsync(IRawSqlSpecification rawSqlSpecification)
		{
			return await _сontext.Database.GetDbConnection().ExecuteScalarAsync<int>(rawSqlSpecification.RawSql, rawSqlSpecification.Params);
		}
		public async Task<int> Exec(IRawSqlSpecification rawSqlSpecification)
		{
			return await _сontext.Database.GetDbConnection().ExecuteAsync(rawSqlSpecification.RawSql, rawSqlSpecification.Params);
		}
		private IQueryable<TEntity> ApplySpec(ISpecification<TEntity> specification = null)
		{
			return SpecificationEvaluator.GetQuery(_entities.AsQueryable(), specification);
		}
		private IQueryable<TProjectedType> ApplySpec<TProjectedType>(IProjectedSpecification<TEntity, TProjectedType> specification)
			where TProjectedType : class
		{
			return SpecificationEvaluator.GetQuery(_entities.AsQueryable(), specification);
		}
	}
}