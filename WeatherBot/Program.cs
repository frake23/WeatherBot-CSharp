using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Args;
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
            await _bot.SendTextMessageAsync(410720842, "123");
            _bot.StartReceiving();
            Thread.CurrentThread.Join();
        }

        private static async void BotOnMessage(object sender, MessageEventArgs e)
        {
            var msg = e?.Message;

            var id = msg.Chat.Id;
            var username = msg.Chat.Username;
            
            var text = msg.Text;
            var user = new User(id, username);
            Console.Write(user.Id);
            await _database.AddUserById(user);
        }
    }
}
