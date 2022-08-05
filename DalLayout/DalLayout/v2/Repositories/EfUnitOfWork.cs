using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace DalLayout.v2.Repositories
{
	public abstract class EfUnitOfWork<TContext> : IEfUnitOfWork<TContext> where TContext : DbContext
	{
		private readonly TContext _context;
		private bool _disposed = false;
		private IDbContextTransaction _transaction;
		protected EfUnitOfWork(TContext context)
		{
			_context = context;
		}

		public void BeginTransaction()
		{
			_transaction = _context.Database.BeginTransaction();
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
		public virtual void Dispose(bool disposing)
		{
			if (_disposed) { return; }
			if (disposing)
			{
				if (_transaction != null)
					_transaction.Dispose();
				_context.Dispose();
			}
			_disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
