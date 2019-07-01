using Newtonsoft.Json;

namespace WeatherBot.Api.OpenWeatherMap.Json.CurrentWeather.Parts
{
    public class Coord
    {
        [JsonProperty(PropertyName = "lon")]
        public float Lon { get; set; }
        
        [JsonProperty(PropertyName = "lat")]
        public float Lat { get; set; }
    }
}