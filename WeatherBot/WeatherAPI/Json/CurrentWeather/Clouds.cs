using Newtonsoft.Json;

namespace WeatherBot.WeatherAPI.Json.CurrentWeather
{
    public class Clouds
    {
        [JsonProperty(PropertyName = "all")]
        public string All { get; set; }
    }
}