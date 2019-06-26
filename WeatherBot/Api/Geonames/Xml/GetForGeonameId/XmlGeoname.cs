using System.Xml.Serialization;

namespace WeatherBot.Api.Geonames.Xml.GetForGeonameId
{
    [XmlRoot("geoname")]
    public class XmlGeoname
    {
        [XmlElement("geonameId")]
        public int GeonameId { get; set; }
        
        [XmlElement("name")]
        public string Name { get; set; }
       
        [XmlElement("adminName1")]
        public string AdminName { get; set; }
        
        [XmlElement("countryCode")]
        public string CountryCode { get; set; }
        
        [XmlElement("lat")]
        public float Latitude { get; set; }
        
        [XmlElement("lng")]
        public float Longitude { get; set; }
        
        [XmlElement("timezone")]
        public string Timezone { get; set; }
    }
}