using Newtonsoft.Json;

namespace WeatherBot.Api.OpenWeatherMap.Json.CurrentWeather.Parts
{
    public class Main
    {
        [JsonProperty(PropertyName = "temp")]
        public float Temp { get; set; }
        
        [JsonProperty(PropertyName = "pressure")]
        public float Pressure { get; set; }
        
        [JsonProperty(PropertyName = "humidity")]
        public int Humidity { get; set; }
        
        [JsonProperty(PropertyName = "temp_min")]
        public float TempMin { get; set; }
        
        [JsonProperty(PropertyName = "temp_max")]
        public float TempMax { get; set; }
    }
}