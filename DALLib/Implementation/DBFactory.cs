using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib.Implementation
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
