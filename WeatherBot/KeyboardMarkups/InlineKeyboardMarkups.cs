using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.KeyboardMarkups
{
    internal static class InlineKeyboardMarkups
    {
        private static InlineKeyboardButton BackButton(string callbackData)
        {
            return InlineKeyboardButton.WithCallbackData("⬅", callbackData);
        }
        internal static InlineKeyboardMarkup MainInlineMarkup(string lang)
        {
            return new InlineKeyboardMarkup(new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(KeyboardMarkupsText.Text["CurrentWeatherText"][lang], "getCurrentWeather"),
                    InlineKeyboardButton.WithCallbackData(KeyboardMarkupsText.Text["FewDaysForecastText"][lang], "getFewDaysForecast") 
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(KeyboardMarkupsText.Text["SettingsText"][lang], "settings")
                }
            });
        }
        internal static InlineKeyboardMarkup SettingsInlineMarkup(string lang)
        {
            return new InlineKeyboardMarkup(new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(KeyboardMarkupsText.Text["ForecastDistributionText"][lang], "forecastDistribution")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(KeyboardMarkupsText.Text["GeolocationText"][lang], "setGeolocation")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(KeyboardMarkupsText.Text["SelectLanguageText"][lang], "selectLanguage")
                },
                new[]
                {
                    BackButton("backToMain")
                }
            });
        }
        internal static InlineKeyboardMarkup LanguageInlineMarkup()
        {
            return new InlineKeyboardMarkup(new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(KeyboardMarkupsText.Text["SelectLanguageText"]["ru"], "russianLanguage"),
                    InlineKeyboardButton.WithCallbackData(KeyboardMarkupsText.Text["SelectLanguageText"]["en"], "englishLanguage")
                },
                new []
                {
                    BackButton("backToSettings")
                }
            });
        }
    }
}