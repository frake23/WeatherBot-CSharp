using Newtonsoft.Json;

namespace WeatherBot.Api.OpenWeatherMap.Json.CurrentWeather.Parts
{
    public class Weather
    {
        [JsonProperty(PropertyName = "lon")]
        public int Id { get; set; }
        
        [JsonProperty(PropertyName = "main")]
        public string Main { get; set; }
        
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        
        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }
    }
}