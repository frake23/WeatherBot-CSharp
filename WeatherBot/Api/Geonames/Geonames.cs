using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using WeatherBot.Api.Geonames.Json;
using WeatherBot.Api.Geonames.Xml;
using WeatherBot.Request;

namespace WeatherBot.Api.Geonames
{
    internal class Geonames
    {
        internal Geonames(string[] tokens)
        {
            _tokens = tokens;
        }

        private string[] _tokens;
        
        private const string SearchUri = "http://api.geonames.org/searchJSON?";
        private const string GetUri = "http://api.geonames.org/get?";
        private const string FindNearbyPlaceNameUri = "http://api.geonames.org/findNearbyPlaceNameJSON?";

        internal async Task<JsonGeonames> SearchCity(string name, string country, string lang, string featureClass="p", int maxRows=5)
        {
            var parameters = new Dictionary<string, string>
            {
                {"name", name},
                {"country", country},
                {"lang", lang},
                {"maxRows", maxRows.ToString()},
                {"featureClass", featureClass},
                {"username", ""}
            };
            foreach (var token in _tokens)
            {
                try
                {
                    parameters["username"] = token;
                    var request = new JsonRequest<JsonGeonames>(SearchUri, parameters);
                    return await request.GetDeserialized();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
            return null;
        }
        
        internal async Task<JsonGeonames> NearbyPlacename(float latitude, float longitude, string lang, string cities="cities15000")
        {
            var parameters = new Dictionary<string, string>
            {
                {"lat", latitude.ToString("0.000000", CultureInfo.InvariantCulture)},
                {"lng", longitude.ToString("0.000000", CultureInfo.InvariantCulture)},
                {"cities", cities},
                {"lang", lang},
                {"username", ""}
            };
            foreach (var token in _tokens)
            {
                try
                {
                    parameters["username"] = token;
                    var request = new JsonRequest<JsonGeonames>(FindNearbyPlaceNameUri, parameters);
                    return await request.GetDeserialized();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
            return null;
        }

        internal async Task<XmlGeoname> CityForGeonameId(int geonameId, string lang)
        {
            var parameters = new Dictionary<string, string>
            {
                {"geonameId", geonameId.ToString()},
                {"lang", lang}
            };
            foreach (var token in _tokens)
            {
                try
                {
                    parameters["username"] = token;
                    var request = new XmlRequest<XmlGeoname>(GetUri, parameters);
                    return await request.GetDeserialized();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return null;
        }
    }
}