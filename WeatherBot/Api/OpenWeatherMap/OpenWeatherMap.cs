using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using WeatherBot.Api.OpenWeatherMap.Json.CurrentWeather;
using WeatherBot.Request;

namespace WeatherBot.Api.OpenWeatherMap
{
    internal class OpenWeatherMap
    {
        internal OpenWeatherMap(string[] tokens)
        {
            _tokens = tokens;
        }

        private string[] _tokens;

        private const string CurrentWeatherUri = "api.openweathermap.org/data/2.5/weather?";
        
        internal async Task<JsonCurrentWeather> CurrentWeather(float latitude, float longitude, string lang, string units="metric")
        {
            var parameters = new Dictionary<string, string>
            {
                {"lat", latitude.ToString("0.000000", CultureInfo.InvariantCulture)},
                {"lon", longitude.ToString("0.000000", CultureInfo.InvariantCulture)},
                {"units", units},
                {"lang", lang},
                {"appid", ""}
            };
            foreach (var token in _tokens)
            {
                try
                {
                    parameters["appid"] = token;
                    var request = new JsonRequest<JsonCurrentWeather>(CurrentWeatherUri, parameters);
                    return await request.GetDeserialized();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return null;
        }
    }
}