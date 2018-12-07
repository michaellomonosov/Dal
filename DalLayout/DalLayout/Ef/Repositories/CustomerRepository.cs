using DalLayout.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalLayout.Ef.Repositories
{
    public class CustomerRepository : EfRepository<Customer, int, DbContext>
    {
        public CustomerRepository(IEfUnitOfWork<DbContext> uow) : base(uow)
        {
        }
    }
}
