using DalLayout.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DalLayout.Dapper
{
    public interface IDapperRepository<TEntity, TKey, TContext> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TContext : NativeDbContext
    {
        void Delete(TKey id);
        void Delete(IEnumerable<TKey> id);

        Task DeleteAsync(TKey id);
        Task DeleteAsync(IEnumerable<TKey> id);
    }
}
