using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherBot.WeatherAPI
{
    internal static class WeatherApi
    {
        public static async Task<string> NowWeather()
        {
            var parameters = new Dictionary<string, string>
            {
                {"APPID", "b6907d289e10d714a6e88b30761fae22"}, {"id", "524901"}
            };
            return await Requests.GetString("https://samples.openweathermap.org/data/2.5/weather", parameters);
        }
    }
}
