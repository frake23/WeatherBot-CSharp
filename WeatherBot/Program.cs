using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using WeatherBot.Database;
using WeatherBot.Database.Models;

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
            _bot.OnCallbackQuery += BotOnLanguageCallbackQuery;
            _bot.StartReceiving();
            Thread.CurrentThread.Join();
        }

        private static void BotOnLanguageCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            return;        
        }

        private static async void BotOnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var callbackQuery = e.CallbackQuery;
            var messageId = callbackQuery.Message.MessageId;
            var id = callbackQuery.From.Id;
            var user = await _database.GetUserById(id);
            
            switch (callbackQuery.Data)
            {
                case "back":
                {
                    await _bot.EditMessageTextAsync(id, messageId, "1", replyMarkup: Markups.MainMarkup(user.Lang));
                    break;
                }
                case "setGeolocation":
                {
                    await _bot.EditMessageTextAsync(id, messageId, "1", replyMarkup: Markups.GeolocationMarkup(user.Lang));
                    break;
                }
                case "selectLanguage":
                {
                    await _bot.EditMessageTextAsync(id, messageId, "2", replyMarkup: Markups.LanguageMarkup());
                    break;
                }
                case "englishLanguage":
                {
                    await _database.UpdateLanguage(id, "en");
                    await _bot.EditMessageTextAsync(id, messageId, "3", replyMarkup: Markups.MainMarkup("en"));
                    break;
                }
                case "russianLanguage":
                {
                    await _database.UpdateLanguage(id, "ru");
                    await _bot.EditMessageTextAsync(id, messageId, "3", replyMarkup: Markups.MainMarkup("ru"));
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
                        switch (commandArgs[0])
                        {
                            case "/start":
                            {
                                await _bot.SendTextMessageAsync(id,"1", replyMarkup: Markups.MainMarkup(user.Lang));
                                break;
                            }
                            default:
                            {
                                
                                break;
                            }
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
