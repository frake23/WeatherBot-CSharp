using Newtonsoft.Json;

namespace WeatherBot.Weather.Json.CurrentWeather
{
    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public double Speed { get; set; }

        [JsonProperty(PropertyName = "deg")]
        public int Deg { get; set; }
    }
}