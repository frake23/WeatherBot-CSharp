using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WeatherBot
{
    internal static class MessageText
    {
        internal static Dictionary<string, Dictionary<string, string>> Text;

        static MessageText()
        {
            Text = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(
                File.ReadAllText(@"TextJson/MessageText.json"));
        }
    }
}