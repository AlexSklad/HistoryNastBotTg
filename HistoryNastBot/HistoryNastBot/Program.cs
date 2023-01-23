using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InlineQueryResults;

namespace HistoryNastBot
{

    class Program
    {
        static BotHelper bot = new BotHelper(token: "TOKEN");
        //public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        //{
        //    // Некоторые действия
        //    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        //    if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        //    {
        //        var message = update.Message;
        //        if (message.Text.ToLower() == "/website")
        //        {
        //            await botClient.SendTextMessageAsync(message.Chat, /*String.Format(< a href = */"https://zen.yandex.ru/cool_history"/* > Кул Хистори | История и мир </ a >)*/);
        //            return;
        //        }
        //        if (message.Text.ToLower() == "/start")
        //        {
        //            await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");
        //            return;
        //        }
        //        await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
        //    }
        //}

        //public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        //{
        //    // Некоторые действия
        //    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        //}

        //private static async void BotOnMessageReceived(object sender, Update messageEventArgs)
        //{
        //    var message = messageEventArgs.Message;

        //    if (message == null || message.Type != MessageType.Text) return;

        //    switch (message.Text)
        //    {
        //        case "":
        //            {
        //                break;
        //            }
        //        default:
        //            {
        //                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

        //                var inlineKeyboard = new InlineKeyboardMarkup(new[]
        //                {
        //                new []
        //                {
        //                    InlineKeyboardButton.WithCallbackData("Support"),
        //                }
        //            });

        //                await bot.SendTextMessageAsync(
        //                    message.Chat.Id,
        //                    "Main Menu",
        //                    replyMarkup: inlineKeyboard);
        //                break;
        //            }
        //    }
        //}
        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetBotName());

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };

            //bot.StartReceiving(
            //    HandleUpdateAsync,
            //    HandleErrorAsync,
            //    receiverOptions,
            //    cancellationToken
            //);

            TelegramBotClient client = bot.GetClient();
            client.StartReceiving(
                bot.HandleUpdateAsync,
                bot.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

        exit:  if (Console.ReadLine().ToLower() != "exit") goto exit;
        }
    }
}
