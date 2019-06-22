using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace WeatherBot.KeyboardMarkups
{
    internal static class KeyboardMarkupsText
    {
        internal static Dictionary<string, Dictionary<string, string>> Text;

        static KeyboardMarkupsText()
        {
            Text = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(
                File.ReadAllText(@"TextJson/KeyboardMarkupsText.json"));
        }
    }
}