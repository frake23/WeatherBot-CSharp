using Newtonsoft.Json;

namespace WeatherBot.Api.OpenWeatherMap.Json.CurrentWeather.Parts
{
    public class Clouds
    {
        [JsonProperty(PropertyName = "all")]
        public int All { get; set; }
    }
}