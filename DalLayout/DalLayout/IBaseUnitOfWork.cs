namespace DalLayout
{
    public interface IBaseUnitOfWork<TContext>
    {
        TContext DbContext { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
