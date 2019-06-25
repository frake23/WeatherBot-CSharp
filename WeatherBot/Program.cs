using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherBot.Api.Geonames;
using WeatherBot.Database;
using WeatherBot.KeyboardMarkups;
using WeatherBot.TextJson;

namespace WeatherBot
{
    internal static class Program
    {
        private static ITelegramBotClient _bot;
        private static Database<SqliteConnection> _database;
        
        private static Text _messageText;
        private static Text _keyboardMarkupsText;
        
        public static async Task Main()
        {
            _messageText = new Text(Config.MessageTextJsonPath);
            _keyboardMarkupsText = new Text(Config.KeyboardMarkupsTextPath);
            
            _bot = new TelegramBotClient(Config.BotToken, new HttpToSocks5Proxy("127.0.0.1", 9150));
            _database = new Database<SqliteConnection>(Config.SqliteConnectionString);
            
            await _bot.DeleteWebhookAsync();
            _bot.OnMessage += BotOnMessage;
            _bot.OnCallbackQuery += BotOnCallbackQuery;
            _bot.StartReceiving();
            Thread.CurrentThread.Join();
        }

        private static async void BotOnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var callbackQuery = e.CallbackQuery;
            var messageId = callbackQuery.Message.MessageId;
            var id = callbackQuery.From.Id;
            var user = await _database.GetUserById(id);
            var lang = user.Lang;

            switch (callbackQuery.Data)
            {
                case "backToMain":
                {
                    await _bot.EditMessageTextAsync(id, messageId, _messageText.Json["MainText"][lang],
                        replyMarkup: InlineKeyboardMarkups.MainInlineMarkup(lang));
                    break;
                }
                case "backToSettings":
                {
                    await _bot.EditMessageTextAsync(id, messageId, _messageText.Json["SettingsText"][lang],
                        replyMarkup: InlineKeyboardMarkups.SettingsInlineMarkup(lang));
                    break;
                }
                case "settings":
                {
                    await _bot.EditMessageTextAsync(id, messageId, _messageText.Json["SettingsText"][lang],
                        replyMarkup: InlineKeyboardMarkups.SettingsInlineMarkup(lang));
                    break;
                }
                case "setGeolocation":
                {
                    var cityId = user.CityId;
                    await _bot.EditMessageTextAsync(id, messageId, _messageText.Json["SavingDataText"][lang],
                        ParseMode.Html);
                    if (cityId == null)
                    {
                        await _bot.SendTextMessageAsync(id, _messageText.Json["GeolocationIsNullText"][lang],
                            ParseMode.Html, replyMarkup: ReplyKeyboardMarkups.LocationReplyMarkup(lang));

                    }
                    else
                    {
                        var city = await _database.GetCityById((long) cityId);
                        await _bot.SendLocationAsync(id, city.Latitude, city.Longitude,
                            replyMarkup: ReplyKeyboardMarkups.LocationReplyMarkup(lang));
                    }
                    await _database.UpdateGeoState(id, 1);
                    break;
                }
                case "selectLanguage":
                {
                    await _bot.EditMessageTextAsync(id, messageId, _messageText.Json["SelectLanguageText"][lang],
                        replyMarkup: InlineKeyboardMarkups.LanguageInlineMarkup());
                    break;
                }
                case "englishLanguage":
                {
                    await _database.UpdateLanguage(id, "en");
                    await _bot.EditMessageTextAsync(id, messageId, _messageText.Json["SettingsText"]["en"],
                        replyMarkup: InlineKeyboardMarkups.SettingsInlineMarkup("en"));
                    break;
                }
                case "russianLanguage":
                {
                    await _database.UpdateLanguage(id, "ru");
                    await _bot.EditMessageTextAsync(id, messageId, _messageText.Json["SettingsText"]["ru"],
                        replyMarkup: InlineKeyboardMarkups.SettingsInlineMarkup("ru"));
                    break;
                }
            }
        }

        private static async void BotOnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                var msg = e?.Message;
                if (msg == null)
                    return;
                if (msg.Chat.Type != ChatType.Private)
                    return;

                var id = msg.Chat.Id;
                var username = msg.Chat.Username;
                var user = await _database.GetUserById(id) ?? await _database.AddUser(id, username);
                var lang = user.Lang;
                var geoState = user.GeoState;
                
                switch (msg.Type)
                {
                    case MessageType.Text:
                    {
                        var text = msg.Text;
                        var commandArgs = text.Split();
                        if (geoState == 0)
                        {
                            if (commandArgs[0].Equals("/start") || commandArgs[0].Equals("/help"))
                            {
                                await _bot.SendTextMessageAsync(id, _messageText.Json["MainText"][lang],
                                    replyMarkup: InlineKeyboardMarkups.MainInlineMarkup(lang));
                            }
                        }
                        else if (geoState == 1)
                        {
                           if (text == "⬅")
                           {
                                await _bot.SendTextMessageAsync(id, _messageText.Json["SettingsText"][lang],
                                    replyMarkup: InlineKeyboardMarkups.SettingsInlineMarkup(lang));
                                await _database.UpdateGeoState(id, 0);
                           }
                           else if (text == _keyboardMarkupsText.Json["FindCityText"]["ru"] ||
                                      text == _keyboardMarkupsText.Json["FindCityText"]["en"])
                           {
                               await _bot.SendTextMessageAsync(id, _messageText.Json["FindCityText"][lang],
                                   ParseMode.Html);
                               await _database.UpdateGeoState(id, 2);
                           }
                        }
                        else if (geoState == 2)
                        {
                            var placenameArgs = text.Split(", ");
                            var geonames = new Geonames(Config.GeonamesToken);
                            var jsonSearch = await geonames.FindCity(placenameArgs[0], placenameArgs[1], lang, 5, "p");
                            await _database.UpdateGeoState(id, 0);
                            await _bot.SendTextMessageAsync(id, "1", replyMarkup: InlineKeyboardMarkups.CitiesKeyboardMarkup(jsonSearch));
                        }
                        break;
                    }
                    case MessageType.Location:
                    {
                        if (geoState == 1)
                        {
                            var location = msg.Location;
                            await _database.UpdateUserCityIdByCoordinates(id, location.Latitude, location.Longitude,
                                lang);
                            await _database.UpdateGeoState(id, 0);
                            await _bot.SendTextMessageAsync(id, _messageText.Json["SettingsText"][lang],
                                replyMarkup: InlineKeyboardMarkups.SettingsInlineMarkup(lang));
                        }
                        break;
                    }
                    default:
                        return;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
