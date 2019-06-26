using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherBot.Api.Geonames.Json.Search;
using WeatherBot.TextJson;

namespace WeatherBot.KeyboardMarkups
{
    internal static class InlineKeyboardMarkups
    {
        private static Text _keyboardMarkupsText = new Text(Config.KeyboardMarkupsTextPath);
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
                    InlineKeyboardButton.WithCallbackData(_keyboardMarkupsText.Json["CurrentWeatherText"][lang], "getCurrentWeather"),
                    InlineKeyboardButton.WithCallbackData(_keyboardMarkupsText.Json["FewDaysForecastText"][lang], "getFewDaysForecast") 
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(_keyboardMarkupsText.Json["SettingsText"][lang], "settings")
                }
            });
        }
        
        internal static InlineKeyboardMarkup SettingsInlineMarkup(string lang)
        {
            return new InlineKeyboardMarkup(new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(_keyboardMarkupsText.Json["ForecastDistributionText"][lang], "forecastDistribution")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(_keyboardMarkupsText.Json["GeolocationText"][lang], "setGeolocation")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(_keyboardMarkupsText.Json["SelectLanguageText"][lang], "selectLanguage")
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
                    InlineKeyboardButton.WithCallbackData(_keyboardMarkupsText.Json["SelectLanguageText"]["ru"], "russianLanguage"),
                    InlineKeyboardButton.WithCallbackData(_keyboardMarkupsText.Json["SelectLanguageText"]["en"], "englishLanguage")
                },
                new []
                {
                    BackButton("backToSettings")
                }
            });
        }

        internal static InlineKeyboardMarkup CitiesKeyboardMarkup(JsonSearch jsonSearch)
        {
            return new InlineKeyboardMarkup(
                jsonSearch.Geonames.Select(geoname => new [] {InlineKeyboardButton.WithCallbackData(geoname.Name + ", " + geoname.AdminName, geoname.GeonameId.ToString())}).Append(new []{BackButton("backToSettingsWithZeroGeostate")})
            );
        }
    }
}