using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherBot.Request
{
    internal class JsonRequest<T>: Request<T>
    {


        internal JsonRequest(string uri, Dictionary<string, string> parameters) : base(uri, parameters)
        {
        }
        internal override async Task<T> GetDeserialized()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var uriString = await GetString();
            return JsonConvert.DeserializeObject<T>(uriString, jsonSerializerSettings);
        }
    }
}