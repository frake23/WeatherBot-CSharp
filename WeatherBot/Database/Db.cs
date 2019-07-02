using System.Linq;
using System.Threading.Tasks;
using WeatherBot.Database.Models;
using WeatherBot.Database.Connector;
using Dapper;

namespace WeatherBot.Database
{
    public class Db
    {
        public Db(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }
        
        private readonly IDbConnector _dbConnector;
        public async Task<User> AddUser(long id, string username)
        {
            using (var conn = _dbConnector.Connection())
            {
                const string sql = @"INSERT INTO users (id, username) VALUES (@id, @username)";
                await conn.ExecuteAsync(sql, new {id, username});
            }
            return await GetUserById(id);
        }

        public async Task<User> GetUserById(long id)
        {
            using (var conn = _dbConnector.Connection())
            {
                const string sql = @"SELECT * FROM users WHERE id = @id";
                var query = await conn.QueryAsync<User>(sql, new {id});
                return query.FirstOrDefault();
            }
        }

        public async Task UpdateLanguage(long id, string lang)
        {
            using (var conn = _dbConnector.Connection())
            {
                const string sql = @"UPDATE users SET lang = @lang WHERE id = @id";
                await conn.ExecuteAsync(sql, new {lang, id});
            }
        }
        
        public async Task UpdateGeoState(long id, int geoState)
        {
            using (var conn = _dbConnector.Connection())
            {
                const string sql = @"UPDATE users SET geoState = @geoState WHERE id = @id";
                await conn.ExecuteAsync(sql, new {geoState, id});
            }
        }

        private async Task<City> GetCityByCoordinates(float latitude, float longitude, string lang)
        {
            using (var conn = _dbConnector.Connection())
            {
                const string sql = @"SELECT id FROM cities WHERE latitude = @latitude and longitude = @longitude and lang = @lang";
                var query = await conn.QueryAsync<City>(sql, new {latitude, longitude, lang});
                return query.FirstOrDefault();
            }
        }

        public async Task<City> AddCity(int geonameId, string name, string adminName, string countryCode,
            float latitude, float longitude, string lang, string timezone)
        {
            using (var conn = _dbConnector.Connection())
            {
                const string sql =
                    @"INSERT INTO cities (geonameId, name, adminName, countryCode, latitude, longitude, lang, timezone) VALUES (@geonameId, @name, @adminName, @countryCode, @latitude, @longitude, @lang, @timezone)";
                await conn.ExecuteAsync(sql,
                    new {geonameId, name, adminName, countryCode, latitude, longitude, lang, timezone});
            }
            return await GetCityByGeonameId(geonameId, lang);
        }
        
        public async Task UpdateUserCityId(long id, int cityId)
        {
            using (var conn = _dbConnector.Connection())
            {
                const string sql = @"UPDATE users SET cityId = @cityId WHERE id = @id";
                await conn.ExecuteAsync(sql, new {cityId, id});
            }
        }
        
        public async Task<City> GetCityById(int id)
        {
            using (var conn = _dbConnector.Connection())
            {
                const string sql = @"SELECT * FROM cities WHERE id = @id";
                var query = await conn.QueryAsync<City>(sql, new {id});
                return query.FirstOrDefault();
            }
        }
        
        public async Task<City> GetCityByGeonameId(int geonameId, string lang)
        {
            using (var conn = _dbConnector.Connection())
            {
                const string sql = @"SELECT * FROM cities WHERE geonameId = @geonameId and lang = @lang";
                var query = await conn.QueryAsync<City>(sql, new {geonameId, lang});
                return query.FirstOrDefault();
            }
        }

        public async Task UpdateCityCurrentWeather(long currentWeatherUpdatedTime, string currentWeather, int id)
        {
            using (var conn = _dbConnector.Connection())
            {
                const string sql =
                    @"UPDATE cities SET currentWeather = @currentWeather, currentWeatherUpdatedTime = @currentWeatherUpdatedTime WHERE id = @id";
                await conn.ExecuteAsync(sql, new {currentWeather, currentWeatherUpdatedTime, id});
            }
        }
    }
}