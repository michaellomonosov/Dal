using Microsoft.EntityFrameworkCore;

namespace DalLayout.v2.Repositories
{
	public interface IEfRepository<TEntity, TContext> : IRepository<TEntity>
		where TEntity : class
		where TContext : DbContext
	{
	}
}