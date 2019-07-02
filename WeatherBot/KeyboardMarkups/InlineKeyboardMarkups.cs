using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherBot.Api.Geonames.Json;
using WeatherBot.TextJson;

namespace WeatherBot.KeyboardMarkups
{
    internal class InlineKeyboardMarkups
    {
        internal InlineKeyboardMarkups(Text keyboardMarkupsText)
        {
            _keyboardMarkupsText = keyboardMarkupsText;
        }

        private readonly Text _keyboardMarkupsText;
        private static InlineKeyboardButton BackButton(string callbackData)
        {
            return InlineKeyboardButton.WithCallbackData("⬅", callbackData);
        }
        internal InlineKeyboardMarkup MainInlineMarkup(string lang)
        {
            return new InlineKeyboardMarkup(new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(_keyboardMarkupsText.Json["CurrentWeatherText"][lang], "currentWeather"),
                    InlineKeyboardButton.WithCallbackData(_keyboardMarkupsText.Json["FewDaysForecastText"][lang], "fewDaysForecast") 
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(_keyboardMarkupsText.Json["SettingsText"][lang], "settings")
                }
            });
        }
        
        internal InlineKeyboardMarkup SettingsInlineMarkup(string lang)
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
        
        internal InlineKeyboardMarkup LanguageInlineMarkup()
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

        internal static InlineKeyboardMarkup CitiesInlineMarkup(JsonGeonames jsonGeonames)
        {
            return new InlineKeyboardMarkup(
                jsonGeonames.Geonames.Select(geoname => new [] {InlineKeyboardButton.WithCallbackData($"{geoname.Name}, {geoname.AdminName}", geoname.GeonameId.ToString())}).Append(new []{BackButton("backToSettingsWithZeroGeostate")})
            );
        }

        internal InlineKeyboardMarkup BackFromCurrentWeatherInlineMarkup()
        {
            return new InlineKeyboardMarkup(new []
            {
                BackButton("backToMain")
            });
        }
    }
}