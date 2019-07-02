using System;
using System.Threading.Tasks;
using WeatherBot.Api.Geonames;
using WeatherBot.Api.OpenWeatherMap;
using WeatherBot.Api.OpenWeatherMap.Json.CurrentWeather;
using WeatherBot.Database;
using WeatherBot.TextJson;

namespace WeatherBot.Utils
{
    internal class Weather
    {
        internal Weather(string[] owmTokens, Text weatherText, Db db)
        {
            _openWeatherMap = new OpenWeatherMap(owmTokens);
            _weatherText = weatherText;
            _db = db;
        }
        
        private readonly Text _weatherText;
        private readonly Db _db;
        private readonly OpenWeatherMap _openWeatherMap;

        private string CurrentWeatherToString(JsonCurrentWeather currentWeather, string cityName, string lang)
        {
            return string.Format(_weatherText.Json["CurrentWeatherText"][lang], cityName,
                currentWeather.Weather[0].Description, Math.Round(currentWeather.Main.Temp), currentWeather.Wind.Speed,
                Math.Round(currentWeather.Main.Pressure / 1.333), currentWeather.Main.Humidity,
                currentWeather.Clouds.All);
        }
        
        internal async Task<string> CurrentWeatherText(int cityId)
        {
            var city = await _db.GetCityById(cityId);
            var lang = city.Lang;
            var currentWeatherText = city.CurrentWeather;
            var now = Time.Now();
            if (now - city.CurrentWeatherUpdatedTime > 3600)
            {
                var currentWeather = await _openWeatherMap.CurrentWeather(city.Latitude, city.Longitude, lang);
                currentWeatherText = CurrentWeatherToString(currentWeather, city.Name, lang);
                await _db.UpdateCityCurrentWeather(now, currentWeatherText, city.Id);
            }

            return currentWeatherText;
        }
    }
}