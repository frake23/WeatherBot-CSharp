using Newtonsoft.Json;

namespace WeatherBot.Api.OpenWeatherMap.Json.CurrentWeather.Parts
{
    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public float Speed { get; set; }

        [JsonProperty(PropertyName = "deg")]
        public float Deg { get; set; }
    }
}