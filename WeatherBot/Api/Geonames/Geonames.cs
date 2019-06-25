using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherBot.Api.Geonames.Json.Search;
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
            Request<JsonSearch> request;
            foreach (var token in _tokens)
            {
                try
                {
                    parametres["username"] = token;
                    request = new Request<JsonSearch>(SearchUri, parametres);
                    return await request.GetJson();
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