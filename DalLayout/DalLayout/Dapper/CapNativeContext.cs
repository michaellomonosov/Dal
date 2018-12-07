using System.Data;

namespace DalLayout.Dapper
{
    public class CapNativeContext : NativeDbContext
    {
        public CapNativeContext(IDbConnection connection) : base(connection)
        {
        }

        public CapNativeContext(string connectionName) : base(connectionName)
        {
        }
    }
}
