using System;
using System.Data;

namespace DalLayout.Dapper
{
    public class DapperUnitOfWork<TContext> : IDapperUnitOfWork<TContext>, IDisposable where TContext : NativeDbContext
    {
        public TContext DbContext { get; }
        public IDbTransaction Transaction { get; private set; }

        public DapperUnitOfWork(TContext context)
        {
            DbContext = context;
        }

        public void BeginTransaction()
        {
            Transaction = DbContext.Connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                // commit transaction if there is one active
                if (Transaction != null)
                    Transaction.Commit();
            }
            catch
            {
                // rollback if there was an exception
                if (Transaction != null)
                    Transaction.Rollback();

                throw;
            }
            finally
            {
            }
        }

        public void Rollback()
        {
            try
            {
                if (Transaction != null)
                    Transaction.Rollback();
            }
            finally
            {
            }
        }

        ~DapperUnitOfWork()// the finalizer
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (Transaction != null)
                        Transaction.Dispose();
                    DbContext.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
