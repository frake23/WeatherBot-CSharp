using System;
using WeatherBot.Api.OpenWeatherMap.Json.CurrentWeather;
using WeatherBot.Database;
using WeatherBot.TextJson;

namespace WeatherBot.Utils
{
    internal class Weather
    {
        private Text _weatherText;

        internal Weather(Text weatherText)
        {
            _weatherText = weatherText;
        }
        
        internal string CurrentWeather(JsonCurrentWeather currentWeather, string cityName, string lang)
        {
            return string.Format(_weatherText.Json["CurrentWeatherText"][lang], cityName,
                currentWeather.Weather[0].Description, Math.Round(currentWeather.Main.Temp), currentWeather.Wind.Speed,
                Math.Round(currentWeather.Main.Pressure / 1.333), currentWeather.Main.Humidity,
                currentWeather.Clouds.All);
        }
    }
}