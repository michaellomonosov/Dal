using System.Data;

namespace DalLayout.Dapper
{
    public interface IDapperUnitOfWork<TContext> : IBaseUnitOfWork<TContext> where TContext : NativeDbContext
    {
        IDbTransaction Transaction { get; }
    }
}
