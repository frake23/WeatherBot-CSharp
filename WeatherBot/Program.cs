using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using WeatherBot.Database;
using WeatherBot.KeyboardMarkups;

namespace WeatherBot
{
    internal static class Program
    {
        private static ITelegramBotClient _bot;
        private static Database<SqliteConnection> _database;
        public static async Task Main()
        {
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
                    await _bot.EditMessageTextAsync(id, messageId, MessageText.Text["MainText"][lang], replyMarkup: InlineKeyboardMarkups.MainMarkup(lang));
                    break;
                }
                case "backToSettings":
                {
                    await _bot.EditMessageTextAsync(id, messageId, MessageText.Text["SettingsText"][lang], replyMarkup: InlineKeyboardMarkups.SettingsMarkup(lang));
                    break;
                }
                case "settings":
                {
                    await _bot.EditMessageTextAsync(id, messageId, MessageText.Text["SettingsText"][lang], replyMarkup: InlineKeyboardMarkups.SettingsMarkup(lang));
                    break;
                }
                case "setGeolocation":
                {
                    if (user.CityId == null)
                        await _bot.EditMessageTextAsync(id, messageId, MessageText.Text["GeolocationIsNullText"][lang], ParseMode.Html, replyMarkup: InlineKeyboardMarkups.GeolocationMarkup(lang));
                    
                    break;
                }
                case "requestLocation":
                {
                    break;
                }
                case "findCity":
                {
                    await _bot.EditMessageTextAsync(id, messageId, MessageText.Text["FindCityText"][lang], ParseMode.Html, replyMarkup: InlineKeyboardMarkups.BackToMainMarkup());
                    break;
                }
                case "selectLanguage":
                {
                    await _bot.EditMessageTextAsync(id, messageId, MessageText.Text["SelectLanguageText"][lang], replyMarkup: InlineKeyboardMarkups.LanguageMarkup());
                    break;
                }
                case "englishLanguage":
                {
                    await _database.UpdateLanguage(id, "en");
                    await _bot.EditMessageTextAsync(id, messageId, MessageText.Text["SettingsText"]["en"], replyMarkup: InlineKeyboardMarkups.SettingsMarkup("en"));
                    break;
                }
                case "russianLanguage":
                {
                    await _database.UpdateLanguage(id, "ru");
                    await _bot.EditMessageTextAsync(id, messageId, MessageText.Text["SettingsText"]["ru"], replyMarkup: InlineKeyboardMarkups.SettingsMarkup("ru"));
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
                
                switch (msg.Type)
                {
                    case MessageType.Text:
                    {
                        var text = msg.Text;
                        var commandArgs = text.Split();
                        if (commandArgs[0].Equals("/start") || commandArgs.Equals("/help"))
                        {
                            await _bot.SendTextMessageAsync(id, MessageText.Text["MainText"][user.Lang], replyMarkup: InlineKeyboardMarkups.MainMarkup(user.Lang));
                        }
                        else
                        {
                            
                        }
                        break;
                    }
                    case MessageType.Location:
                        break;
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
