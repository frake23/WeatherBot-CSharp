using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace WeatherBot.Request
{
    internal abstract class Request<T>
    {
        internal Request(string uri, Dictionary<string, string> parameters)
        {
            _httpClient = new HttpClient();
            _uri = uri;
            _parameters = parameters;
        }
        
        protected readonly HttpClient _httpClient;
        protected readonly string _uri;
        protected readonly Dictionary<string, string> _parameters;

        protected string BuildUri()
        {
            var builder = new UriBuilder(_uri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var param in _parameters.Keys)
                query[param] = _parameters[param];
            builder.Query = query.ToString();
            return builder.ToString();
        }
        
        protected async Task<string> GetString()
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

        internal abstract Task<T> GetDeserialized();
    }
}
