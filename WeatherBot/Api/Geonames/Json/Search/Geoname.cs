using Newtonsoft.Json;namespace WeatherBot.Api.Geonames.Json.Search{    public class Geoname    {        [JsonProperty(PropertyName = "geonameId")]        public int GeonameId { get; set; }                [JsonProperty(PropertyName = "name")]        public string Name { get; set; }               [JsonProperty(PropertyName = "adminName1")]        public string AdminName { get; set; }                [JsonProperty(PropertyName = "countryCode")]        public string CountryCode { get; set; }                [JsonProperty(PropertyName = "lat")]        public string Latitude { get; set; }                [JsonProperty(PropertyName = "lng")]        public string Longitude { get; set; }    }}