using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WeatherBot.Database.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace WeatherBot.Database
{
    public class Database<T> where T: IDbConnection
    {
        private readonly string _connectionString;
        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<User> AddUser(long id, string username)
        {
            using (var conn = (T) Activator.CreateInstance(typeof(T), _connectionString))
            {
                const string sql = @"INSERT INTO users (id, username) VALUES (@id, @username)";
                await conn.ExecuteAsync(sql, new
                {
                    id,
                    username
                });
            }
            return await GetUserById(id);
        }

        public async Task<User> GetUserById(long id)
        {
            using (var conn = (T) Activator.CreateInstance(typeof(T), _connectionString))
            {
                const string sql = "SELECT * FROM users WHERE id = @id";
                var query = await conn.QueryAsync<User>(sql, new {id});
                return query.FirstOrDefault();
            }
        }

        public async Task UpdateLanguage(long id, string lang)
        {
            using (var conn = (T) Activator.CreateInstance(typeof(T), _connectionString))
            {
                const string sql = "UPDATE users SET lang = @lang WHERE id = @id";
                await conn.ExecuteAsync(sql, new {lang, id});
            }
        }
        
        public async Task UpdateGeoState(long id, int geoState)
        {
            using (var conn = (T) Activator.CreateInstance(typeof(T), _connectionString))
            {
                const string sql = "UPDATE users SET geoState = @geoState WHERE id = @id";
                await conn.ExecuteAsync(sql, new {geoState, id});
            }
        }
    }
}