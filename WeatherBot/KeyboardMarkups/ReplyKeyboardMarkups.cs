using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.KeyboardMarkups
{
    internal static class ReplyKeyboardMarkups
    {
        internal static ReplyKeyboardMarkup LocationReplyMarkup(string lang)
        {
            return new ReplyKeyboardMarkup(new []
            {
                new []
                {
                    KeyboardButton.WithRequestLocation(KeyboardMarkupsText.Text["RequestLocationText"][lang]),
                    new KeyboardButton(KeyboardMarkupsText.Text["FindCityText"][lang])
                },
                new []
                {
                    new KeyboardButton("⬅")
                }
            }, true,true);
        }
    }
}