using Microsoft.Data.SqlClient;
using System.Data;

namespace App.Context
{
    public class DapperContext
    {

        public readonly IConfiguration _configuration;
        public readonly string _connectionString;

        public DapperContext(IConfiguration configuration, string connectionString)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("sqliteConnString");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
