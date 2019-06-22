using Newtonsoft.Json;

namespace WeatherBot.Weather.Json.CurrentWeather
{
    public class Main
    {
        [JsonProperty(PropertyName = "temp")]
        public double Temp { get; set; }
        
        [JsonProperty(PropertyName = "pressure")]
        public double Pressure { get; set; }
        
        [JsonProperty(PropertyName = "humidity")]
        public int Humidity { get; set; }
        
        [JsonProperty(PropertyName = "temp_min")]
        public double TempMin { get; set; }
        
        [JsonProperty(PropertyName = "temp_max")]
        public double TempMax { get; set; }
    }
}