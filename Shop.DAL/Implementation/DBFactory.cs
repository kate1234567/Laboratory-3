using System.Data.Entity.Infrastructure;

namespace Shop.DAL.Implementation
{
    public class DBFactory : IDbContextFactory<DB>
    {
        private string _connectionString;
        public DBFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DB Create()
        {
            return new DB(_connectionString);
        }
    }
}
