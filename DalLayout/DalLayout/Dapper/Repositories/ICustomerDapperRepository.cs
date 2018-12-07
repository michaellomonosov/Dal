using DalLayout.Entity;
using System;

namespace DalLayout.Dapper.Repositories
{
    public interface ICustomerDapperRepository : IDapperRepository<Customer, int, NativeDbContext>
    {
    }
}
