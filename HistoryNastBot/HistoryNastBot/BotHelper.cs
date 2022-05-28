using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    internal class BotHelper
    {
        Random rnd = new Random();

        private string _token;

        private const string SiteText = "Сайт";
        private const string RndHistory = "Случайная историческая ссылка";

        TelegramBotClient _client;
        public BotHelper(string token)
        {
            this._token = token;
            _client = new TelegramBotClient(_token);
        }
        public string GetBotName()
        {
            return _client.GetMeAsync().Result.FirstName;
        }

        public TelegramBotClient GetClient()
        {
            return _client;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!", replyMarkup: GetButtons());
                    return;
                }

                if (message.Text.ToLower() == "/website")
                {
                    await botClient.SendTextMessageAsync(message.Chat, /*String.Format(< a href = */"https://zen.yandex.ru/cool_history"/* > Кул Хистори | История и мир </ a >)*/);
                    return;
                }

                switch (message.Text)
                {
                    case SiteText:  
                        await botClient.SendTextMessageAsync(
                            message.Chat, GetUrlReply("https://zen.yandex.ru/cool_history", "Кул Хистори | История и мир"), ParseMode.Html, replyMarkup: GetButtons());
                        break;
                    case RndHistory:
                        await botClient.SendTextMessageAsync(message.Chat,"Выберите раздел", replyMarkup: GetRegionButtons());
                        break;
                    case "Кнопка 3":
                        await botClient.SendTextMessageAsync(message.Chat, "Тестовая кнопка 3", replyMarkup: GetButtons());
                        break;
                    case "Англия":
                        await botClient.SendTextMessageAsync(message.Chat, GetRndUrlReply("England"), ParseMode.Html, replyMarkup: GetRegionButtons());
                        break;
                    case "Россия":
                        await botClient.SendTextMessageAsync(message.Chat, "Тестовая кнопка 3", replyMarkup: GetRegionButtons());
                        break;
                    case "Меню":
                        await botClient.SendTextMessageAsync(message.Chat, "Возврат в меню", replyMarkup: GetButtons());
                        break;
                    default:
                        await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
                        break;

                }
            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            (
                new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton (SiteText), new KeyboardButton (RndHistory), new KeyboardButton ("Кнопка 3")}
                }
            );
        }

        private static IReplyMarkup GetRegionButtons()
        {
            return new ReplyKeyboardMarkup
            (
                new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton ("Англия"), new KeyboardButton ("Россия"), new KeyboardButton ("Меню")}
                }
            );
        }

        private string GetRndUrlReply(string Name)
        {
            XmlHistoryList historyList = new XmlHistoryList(Name);

            int rndValue = rnd.Next(0, historyList.Links.Count - 1);

            var reply = GetUrlReply(historyList.Links[rndValue].Url, historyList.Links[rndValue].Name);
            return reply;
        }

        private string GetUrlReply(string url, string text)
        {
            var reply = $"<a href=\"{url}\"> {text} </a>\n";
            return reply;
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        //internal void GetUpdate()
        //{
        //    _client = new TelegramBotClient (_token);
        //    var me = _client.GetMeAsync().Result;
        //    if (me != null && !string.IsNullOrEmpty(me.Username))
        //    {
        //        while(true)
        //        {

        //        }
        //    }

        //}
    }
}
