using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DalLayout.Retry;

namespace DalLayout.Dapper
{

    public class NativeDbContext : INativeDbContext, IDisposable
    {
        public IDbConnection Connection { get; private set; }

        public NativeDbContext(IDbConnection connection)
        {
            Connection = connection;
            Connection.Open();
        }

        public NativeDbContext(string connectionName, bool usePolicy)
        {
            if (usePolicy)
            {
                Connection = new ReliableSqlDbConnection(connectionName, new DatabaseCommunicationRetryPolicy());
            }
            else
            {
                Connection = new SqlConnection(connectionName);
            }

            Connection.Open();
        }

        ~NativeDbContext()
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
                    Connection.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
