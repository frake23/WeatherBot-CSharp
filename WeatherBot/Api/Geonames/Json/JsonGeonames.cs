using Newtonsoft.Json;
using WeatherBot.Api.Geonames.Json.Parts;

namespace WeatherBot.Api.Geonames.Json
{
    public class JsonGeonames
    {
        [JsonProperty(PropertyName = "geonames")]
        public Geoname[] Geonames { get; set; }
    }
}