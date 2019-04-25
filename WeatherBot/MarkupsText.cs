using System.Collections.Generic;

namespace WeatherBot
{
    internal static class MarkupsText
    {
        internal static Dictionary<string, string> CurrentWeather { get; } = new Dictionary<string, string>
        {
            {"ru", "Погода сейчас"},
            {"en", "Current weather"}
        };

        internal static Dictionary<string, string> FewDaysForecast { get; } = new Dictionary<string, string>
        {
            {"ru", "Прогноз погоды"},
            {"en", "Weather forecast"}
        };
        
        internal static Dictionary<string, string> ForecastTime { get; } = new Dictionary<string, string>
        {
            {"ru", "Время рассылки"},
            {"en", "Distribution time"}
        };
        
        internal static Dictionary<string, string> Geolocation { get; } = new Dictionary<string, string>
        {
            {"ru", "Геолокация"},
            {"en", "Geolocation"}
        };
        
        internal static Dictionary<string, string> Language { get; } = new Dictionary<string, string>
        {
            {"ru", "Язык 🇷🇺"},
            {"en", "Language 🇬🇧"}
        };
        
        internal static Dictionary<string, string> SelectLanguage { get; } = new Dictionary<string, string>
        {
            {"ru", "Русский 🇷🇺"},
            {"en", "English 🇬🇧"}
        };
        
        internal static Dictionary<string, string> GeolocationCoordinates { get; } = new Dictionary<string, string>
        {
            {"ru", "Отправить координаты"},
            {"en", "Send coordinates"}
        };

        internal static Dictionary<string, string> FindCity { get; } = new Dictionary<string, string>
        {
            {"ru", "Найти город"},
            {"en", "Find city"}
        };
        
        internal static string Back = "⬅";
    }
}