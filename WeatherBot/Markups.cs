using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot
{
    internal static class Markups
    {
        private static InlineKeyboardButton BackButton = InlineKeyboardButton.WithCallbackData(MarkupsText.Back, "back");
        internal static InlineKeyboardMarkup MainMarkup(string lang)
        {
            return new InlineKeyboardMarkup(new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(MarkupsText.CurrentWeather[lang], "getCurrentWeather"),
                    InlineKeyboardButton.WithCallbackData(MarkupsText.FewDaysForecast[lang], "getFewDaysForecast") 
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(MarkupsText.ForecastTime[lang], "setForecastTime"),
                    InlineKeyboardButton.WithCallbackData(MarkupsText.Geolocation[lang], "setGeolocation")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(MarkupsText.Language[lang], "selectLanguage")
                }
            });
        }

        internal static InlineKeyboardMarkup LanguageMarkup()
        {
            return new InlineKeyboardMarkup(new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(MarkupsText.SelectLanguage["ru"], "russianLanguage"),
                    InlineKeyboardButton.WithCallbackData(MarkupsText.SelectLanguage["en"], "englishLanguage")
                },
                new []
                {
                    BackButton
                }
            });
        }

        internal static InlineKeyboardMarkup GeolocationMarkup(string lang)
        {
            return new InlineKeyboardMarkup(new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(MarkupsText.GeolocationCoordinates[lang], "sendCoordinates"),
                    InlineKeyboardButton.WithCallbackData(MarkupsText.FindCity[lang], "findCity")
                },
                new []
                {
                    BackButton
                }
            });
        }
    }
}