using Telegram.Bot.Types.ReplyMarkups;
using WeatherBot.TextJson;

namespace WeatherBot.KeyboardMarkups
{
    internal static class ReplyKeyboardMarkups
    {
        private static Text _keyboardMarkupsText = new Text(Config.KeyboardMarkupsTextPath);
        internal static ReplyKeyboardMarkup LocationReplyMarkup(string lang)
        {
            return new ReplyKeyboardMarkup(new []
            {
                new []
                {
                    KeyboardButton.WithRequestLocation(_keyboardMarkupsText.Json["RequestLocationText"][lang]),
                    new KeyboardButton(_keyboardMarkupsText.Json["FindCityText"][lang])
                },
                new []
                {
                    new KeyboardButton("⬅")
                }
            }, true,true);
        }
    }
}