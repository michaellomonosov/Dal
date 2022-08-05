using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalLayout.v2.Repositories
{
	public interface IEfUnitOfWork<TContext> : IDisposable where TContext : DbContext
	{
		void BeginTransaction();
		void Commit();
		void Rollback();
	}
}
