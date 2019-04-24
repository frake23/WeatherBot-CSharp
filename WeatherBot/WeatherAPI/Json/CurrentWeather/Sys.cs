using Newtonsoft.Json;

namespace WeatherBot.WeatherAPI.Json.CurrentWeather
{
    public class Sys
    {
        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }
        
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        
        [JsonProperty(PropertyName = "message")]
        public double Message { get; set; }
        
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        
        [JsonProperty(PropertyName = "sunrise")]
        public long Sunrise { get; set; }
        
        [JsonProperty(PropertyName = "sunset")]
        public long Sunset { get; set; }
    }
}