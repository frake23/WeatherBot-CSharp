using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.KeyboardMarkups
{
    internal static class ReplyKeyboardMarkups
    {
        internal static ReplyKeyboardMarkup SendLocation(string lang)
        {
            return new ReplyKeyboardMarkup(new []
            {
                new []
                {
                    KeyboardButton.WithRequestLocation(KeyboardMarkupsText.Text["RequestLocationText"][lang]) 
                }
            });
        }
    }
}