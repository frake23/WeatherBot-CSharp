using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace WeatherBot.Request
{
    internal class Request<T>
    {
        internal Request(string uri, Dictionary<string, string> parameters)
        {
            _httpClient = new HttpClient();
            _uri = uri;
            _parameters = parameters;
        }
        
        private readonly HttpClient _httpClient;
        private readonly string _uri;
        private readonly Dictionary<string, string> _parameters;

        private string BuildUri()
        {
            var builder = new UriBuilder(_uri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var param in _parameters.Keys)
                query[param] = _parameters[param];
            builder.Query = query.ToString();
            return builder.ToString();
        }
        
        private async Task<string> GetString()
        {
            var buildUri = BuildUri();
            try
            {
                return await _httpClient.GetStringAsync(buildUri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        internal async Task<T> GetJson()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var uriString = await GetString();
            try
            {
                return JsonConvert.DeserializeObject<T>(uriString, jsonSerializerSettings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }
    }
}
