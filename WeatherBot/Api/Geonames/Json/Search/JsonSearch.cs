using Newtonsoft.Json;

namespace WeatherBot.Api.Geonames.Json.Search
{
    public class JsonSearch
    {
        [JsonProperty(PropertyName = "geonames")]
        public Geoname[] Geonames { get; set; }
    }
}