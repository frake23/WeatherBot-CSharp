using Newtonsoft.Json;

namespace WeatherBot.Weather.Json.CurrentWeather
{
    public class Coord
    {
        [JsonProperty(PropertyName = "lon")]
        public double Lon { get; set; }
        
        [JsonProperty(PropertyName = "lat")]
        public double Lat { get; set; }
    }
}