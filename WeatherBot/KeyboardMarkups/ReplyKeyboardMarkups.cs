using Telegram.Bot.Types.ReplyMarkups;
using WeatherBot.TextJson;

namespace WeatherBot.KeyboardMarkups
{
    internal class ReplyKeyboardMarkups
    {
        internal ReplyKeyboardMarkups(Text keyboardMarkupsText)
        {
            _keyboardMarkupsText = keyboardMarkupsText;
        }
        
        private readonly Text _keyboardMarkupsText;
        internal ReplyKeyboardMarkup LocationReplyMarkup(string lang)
        {
            return new ReplyKeyboardMarkup(new []
            {
                new []
                {
                    KeyboardButton.WithRequestLocation(_keyboardMarkupsText.Json["RequestLocationText"][lang]),
                    new KeyboardButton(_keyboardMarkupsText.Json["SearchCityText"][lang])
                },
                new []
                {
                    new KeyboardButton("⬅")
                }
            }, true, true);
        }
    }
}