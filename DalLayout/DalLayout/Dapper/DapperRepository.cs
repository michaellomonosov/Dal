using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DalLayout.Dapper
{
    public abstract class DapperRepository<TContext> where TContext : NativeDbContext
    {
        protected readonly TContext _context;
        protected readonly IDbTransaction _transaction;

        public DapperRepository(IDapperUnitOfWork<TContext> unitOfWork)
        {
            _context = unitOfWork.DbContext;
            _transaction = unitOfWork.Transaction;
        }

        protected TEntity QueryFirstOrDefault<TEntity>(string sql, object parameters = null)
        {
            return _context.Connection.QueryFirstOrDefault<TEntity>(sql, parameters, transaction: _transaction);
        }

        protected List<TEntity> Query<TEntity>(string sql, object parameters = null)
        {
            return _context.Connection.Query<TEntity>(sql, parameters, transaction: _transaction).ToList();
        }

        protected int Execute(string sql, object parameters = null)
        {
            return _context.Connection.Execute(sql, parameters, transaction: _transaction);
        }

        protected async Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(string sql, object parameters = null)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, parameters, transaction: _transaction);
        }

        protected async Task<List<TEntity>> QueryAsync<TEntity>(string sql, object parameters = null)
        {
            var temp = await _context.Connection.QueryAsync<TEntity>(sql, parameters, transaction: _transaction);
            return temp.ToList();
        }

        protected async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            return await _context.Connection.ExecuteAsync(sql, parameters, transaction: _transaction);
        }

        protected IEnumerable<string> GetColumns<TEntity>()
        {
            return typeof(TEntity)
                    .GetProperties()
                    .Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }
    }
}
