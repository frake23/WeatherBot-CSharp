using Newtonsoft.Json;

namespace WeatherBot.WeatherAPI.Json.CurrentWeather
{
    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public double Speed { get; set; }

        [JsonProperty(PropertyName = "deg")]
        public int Deg { get; set; }
    }
}