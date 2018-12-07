using DalLayout.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DalLayout
{
    public partial interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TEntity GetById(TKey id);

        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);

        List<TEntity> All();

        //async 
        Task<TEntity> GetByIdAsync(TKey id);

        Task InsertAsync(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities);
    }
}
