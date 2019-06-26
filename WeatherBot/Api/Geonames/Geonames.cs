using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherBot.Api.Geonames.Json.Search;
using WeatherBot.Api.Geonames.Xml.GetForGeonameId;
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

        internal async Task<JsonSearch> FindCity(string name, string country, string lang, int maxRows, string featureClass)
        {
            var parametres = new Dictionary<string, string>
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
                    parametres["username"] = token;
                    var request = new JsonRequest<JsonSearch>(SearchUri, parametres);
                    return await request.GetDeserialized();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return null;
        }

        internal async Task<XmlGeoname> GetCityForId(int geonameId, string lang)
        {
            var parametres = new Dictionary<string, string>
            {
                {"geonameId", geonameId.ToString()},
                {"lang", lang}
            };
            foreach (var token in _tokens)
            {
                try
                {
                    parametres["username"] = token;
                    var request = new XmlRequest<XmlGeoname>(GetUri, parametres);
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