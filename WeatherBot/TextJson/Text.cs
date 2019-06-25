using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WeatherBot.TextJson
{
    internal class Text
    {
        internal readonly Dictionary<string, Dictionary<string, string>> Json;

        internal Text(string filePath)
        {
            Json = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(
                File.ReadAllText(filePath));
        }
    }
}