using DalLayout.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalLayout.Dapper.Repositories
{
    public class CustomerDapperRepository : DapperRepository<NativeDbContext>, ICustomerDapperRepository
    {
        public CustomerDapperRepository(IDapperUnitOfWork<NativeDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        public void Delete(Customer entity)
        {
            var sql = "delete from dbo.Customer where Id=@Id";
            Execute(sql, new { Id = entity.Id });
        }

        public void Delete(IEnumerable<Customer> entities)
        {
            foreach (var item in entities)
                Delete(item);
        }

        public void Delete(int id)
        {
            var sql = "delete from dbo.Customer where Id=@Id";
            Execute(sql, new { Id = id });
        }

        public void Delete(IEnumerable<int> ids)
        {
            foreach (var item in ids)
                Delete(item);
        }

        public async Task DeleteAsync(Customer entity)
        {
            var sql = "delete from dbo.Customer where Id=@Id";
            await ExecuteAsync(sql, entity);
        }

        public async Task DeleteAsync(IEnumerable<Customer> entities)
        {
            foreach (var item in entities)
                await DeleteAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "delete from dbo.Customer where Id=@Id";
            await ExecuteAsync(sql, new { Id = id });
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            foreach (var item in ids)
                await DeleteAsync(item);
        }

        public List<Customer> All()
        {
            var sql = "select * from dbo.Customer";
            return _context.Connection.Query<Customer>(sql).ToList();
        }

        public Customer GetById(int id)
        {
            var sql = "select * from dbo.Customer where Id=@Id";
            return QueryFirstOrDefault<Customer>(sql, new { Id = id });
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var sql = "select * from dbo.Customer where Id=@Id";
            return await QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
        }

        public void Insert(Customer entity)
        {
            var columns = GetColumns<Customer>();
            var stringOfColumns = string.Join(", ", columns);
            var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
            var query = $"insert into dbo.{typeof(Customer).Name} ({stringOfColumns}) values ({stringOfParameters})";
            Execute(query, entity);
        }

        public void Insert(IEnumerable<Customer> entities)
        {
            foreach (var item in entities)
                Insert(item);
        }

        public async Task InsertAsync(Customer entity)
        {
            var columns = GetColumns<Customer>();
            var stringOfColumns = string.Join(", ", columns);
            var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
            var query = $"insert into dbo.{typeof(Customer).Name} ({stringOfColumns}) values ({stringOfParameters})";
            await ExecuteAsync(query, entity);
        }

        public async Task InsertAsync(IEnumerable<Customer> entities)
        {
            foreach (var item in entities)
                await InsertAsync(item);
        }

        public void Update(Customer entity)
        {
            var columns = GetColumns<Customer>();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            var query = $"update dbo.{typeof(Customer).Name} set {stringOfColumns} where Id = @Id";

            Execute(query, entity);
        }

        public void Update(IEnumerable<Customer> entities)
        {
            foreach (var item in entities)
                Update(item);
        }

        public async Task UpdateAsync(Customer entity)
        {
            var columns = GetColumns<Customer>();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            var query = $"update dbo.{typeof(Customer).Name} set {stringOfColumns} where Id = @Id";
            await ExecuteAsync(query, entity);
        }

        public async Task UpdateAsync(IEnumerable<Customer> entities)
        {
            foreach (var item in entities)
                await UpdateAsync(item);
        }
    }
}
