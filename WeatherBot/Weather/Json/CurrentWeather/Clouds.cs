using Newtonsoft.Json;

namespace WeatherBot.Weather    .Json.CurrentWeather
{
    public class Clouds
    {
        [JsonProperty(PropertyName = "all")]
        public string All { get; set; }
    }
}