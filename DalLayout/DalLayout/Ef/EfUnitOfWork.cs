using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace DalLayout.Ef
{
    public class EfUnitOfWork<TContext> : IEfUnitOfWork<TContext>, IDisposable where TContext : DbContext
    {
        public TContext DbContext { get; }

        private IDbContextTransaction _transaction;

        public EfUnitOfWork(TContext context)
        {
            DbContext = context;
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                        _transaction.Dispose();
                    DbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            _transaction = DbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                // commit transaction if there is one active
                if (_transaction != null)
                    _transaction.Commit();
            }
            catch
            {
                // rollback if there was an exception
                if (_transaction != null)
                    _transaction.Rollback();

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
                if (_transaction != null)
                    _transaction.Rollback();
            }
            finally
            {
            }
        }
    }
}
