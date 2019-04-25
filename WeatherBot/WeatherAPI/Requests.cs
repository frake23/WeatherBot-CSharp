using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WeatherBot.WeatherAPI
{
    static class Requests
    {
        private static readonly HttpClient HttpClient;

        static Requests()
        {
            HttpClient = new HttpClient();
        }
        
        private static string BuildUri(string uri, Dictionary<string, string> parameters)
        {
            var builder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var param in parameters.Keys)
                query[param] = parameters[param];
            builder.Query = query.ToString();
            return builder.ToString();
        }
        
        public static async Task<string> GetString(string uri, Dictionary<string, string> parameters)
        {
            var buildUri = BuildUri(uri, parameters);
            try
            {
                return await HttpClient.GetStringAsync(buildUri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
