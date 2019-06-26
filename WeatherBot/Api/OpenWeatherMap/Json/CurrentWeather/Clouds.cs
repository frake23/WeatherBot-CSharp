using Newtonsoft.Json;

namespace WeatherBot.Api.OpenWeatherMap.Json.CurrentWeather
{
    public class Clouds
    {
        [JsonProperty(PropertyName = "all")]
        public string All { get; set; }
    }
}