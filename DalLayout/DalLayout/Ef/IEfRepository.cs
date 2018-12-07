using DalLayout.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DalLayout.Ef
{
    public interface IEfRepository<TEntity, TKey, TContext> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TContext : DbContext
    {
        void NativeSql(string sql, params object[] parameters);
        Task NativeSqlAsync(string sql, params object[] parameters);
        string GetTableName();
        IQueryable<TEntity> GetAll();
    }
}
