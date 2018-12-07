using DalLayout.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DalLayout.Ef
{
    public class EfRepository<TEntity, TKey, TContext> : IEfRepository<TEntity, TKey, TContext>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TContext : DbContext
    {
        protected readonly TContext _context;
        protected DbSet<TEntity> _entities;

        public EfRepository(IEfUnitOfWork<TContext> unitOfWork)
        {
            _context = unitOfWork.DbContext;
            _entities = _context.Set<TEntity>();
        }

        public virtual TEntity GetById(TKey id)
        {
            return _entities.SingleOrDefault(x => x.Id.Equals(id));
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            _entities.Add(entity);
            _context.SaveChanges();
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _entities.AddRange(entities);

            _context.SaveChanges();
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _context.SaveChanges();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
                _entities.Remove(entity);

            _context.SaveChanges();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _entities.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            await _entities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _entities.RemoveRange(entities);

            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _entities;
        }

        public void NativeSql(string sql, params object[] parameters)
        {
            _context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public async Task NativeSqlAsync(string sql, params object[] parameters)
        {
            await _context.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public string GetTableName()
        {
            var data = _context.Model.FindEntityType(typeof(TEntity)).Relational();
            return string.IsNullOrEmpty(data.Schema)
                ? $"[dbo].[{data.TableName}]"
                : $"[{data.Schema}].[{data.TableName}]";
        }

        public List<TEntity> All()
        {
            return _entities.ToList();
        }

        //public virtual IQueryable<TEntity> Table
        //{
        //    get
        //    {
        //        return _entities;
        //    }
        //}

        //public virtual IQueryable<TEntity> TableNoTracking
        //{
        //    get
        //    {
        //        return _entities.AsNoTracking();
        //    }
        //}
    }
}
