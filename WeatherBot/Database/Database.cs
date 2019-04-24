using System;
using System.Data;
using System.Threading.Tasks;
using WeatherBot.Database.Models;
using Dapper;

namespace WeatherBot.Database
{
    public class Database<T> where T: IDbConnection
    {
        private readonly string _connectionString;
        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task AddUserById(User user)
        {
            using (var conn = (T) Activator.CreateInstance(typeof(T), _connectionString))
            {
                const string sql = "INSERT INTO users (id, username, lang, cityId, forecastTime, geoState) VALUES (@id, @username, @lang, @cityId, @forecastTime, @geoState)";
                await conn.ExecuteAsync(sql, new
                {
                    id =user.Id,
                    username = user.Username,
                    lang = user.Lang,
                    cityId = user.CityId,
                    forecastTime = user.ForecastTime,
                    geoState = user.GeoState
                });
            }
        }
    }
}