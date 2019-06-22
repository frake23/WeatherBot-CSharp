using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.KeyboardMarkups
{
    internal static class InlineKeyboardMarkups
    {
        private static InlineKeyboardButton BackButton(string callbackData)
        {
            return InlineKeyboardButton.WithCallbackData("⬅", callbackData);
        }
        internal static InlineKeyboardMarkup MainMarkup(string lang)
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
        internal static InlineKeyboardMarkup SettingsMarkup(string lang)
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
        internal static InlineKeyboardMarkup LanguageMarkup()
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

        internal static InlineKeyboardMarkup GeolocationMarkup(string lang)
        {
            return new InlineKeyboardMarkup(new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(KeyboardMarkupsText.Text["RequestLocationText"][lang], "requestLocation"),
                    InlineKeyboardButton.WithCallbackData(KeyboardMarkupsText.Text["FindCityText"][lang], "findCity")
                },
                new []
                {
                    BackButton("backToSettings")
                }
            });
        }
        internal static InlineKeyboardMarkup BackToMainMarkup()
        {
            return new InlineKeyboardMarkup(new []
            {
                new []
                {
                    BackButton("backToMain")
                }
            });
        }
    }
}