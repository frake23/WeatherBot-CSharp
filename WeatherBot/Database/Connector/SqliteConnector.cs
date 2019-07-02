using System.Data;
using Microsoft.Data.Sqlite;

namespace WeatherBot.Database.Connector
{
    public class SqliteConnector: IDbConnector
    {
        public SqliteConnector(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString;
        
        public IDbConnection Connection()
        {
            return new SqliteConnection(_connectionString);
        }
    }
}