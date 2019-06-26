using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeatherBot.Request
{
    internal class XmlRequest<T>: Request<T>
    {
        public XmlRequest(string uri, Dictionary<string, string> parameters) : base(uri, parameters)
        {
        }

        internal override async Task<T> GetDeserialized()
        {
            var ser = new XmlSerializer(typeof(T));
            var uriString = await GetString();
            using (var reader = new StringReader(uriString))
            {
               return (T) ser.Deserialize(reader);
            }
        }
    }
}