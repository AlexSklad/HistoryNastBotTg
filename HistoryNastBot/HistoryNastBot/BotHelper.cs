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

                if (message.Text == null)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "К сожалению, я понимаю только текстовые команды", replyMarkup: GetButtons());
                    return;

                }

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
                    case "Русь":
                        await botClient.SendTextMessageAsync(message.Chat, GetRndUrlReply("Russia"), ParseMode.Html, replyMarkup: GetRegionButtons());
                        break;
                    case "Франция":
                        await botClient.SendTextMessageAsync(message.Chat, GetRndUrlReply("France"), ParseMode.Html, replyMarkup: GetRegionButtons());
                        break;
                    case "Германия":
                        await botClient.SendTextMessageAsync(message.Chat, GetRndUrlReply("Germany"), ParseMode.Html, replyMarkup: GetRegionButtons());
                        break;
                    case "Скандинавия":
                        await botClient.SendTextMessageAsync(message.Chat, GetRndUrlReply("Scandinavia"), ParseMode.Html, replyMarkup: GetRegionButtons());
                        break;
                    case "Ацтеки":
                        await botClient.SendTextMessageAsync(message.Chat, GetRndUrlReply("Aztecs"), ParseMode.Html, replyMarkup: GetRegionButtons());
                        break;
                    case "Иерусалим":
                        await botClient.SendTextMessageAsync(message.Chat, GetRndUrlReply("Jerusalem"), ParseMode.Html, replyMarkup: GetRegionButtons());
                        break;
                    case "Славянские народы":
                        await botClient.SendTextMessageAsync(message.Chat, GetRndUrlReply("Slavs"), ParseMode.Html, replyMarkup: GetRegionButtons());
                        break;
                    case "Шотландия":
                        await botClient.SendTextMessageAsync(message.Chat, GetRndUrlReply("Scotland"), ParseMode.Html, replyMarkup: GetRegionButtons());
                        break;
                    case "Испания":
                        await botClient.SendTextMessageAsync(message.Chat, GetRndUrlReply("Spain"), ParseMode.Html, replyMarkup: GetRegionButtons());
                        break;
                    case "Прочие":
                        await botClient.SendTextMessageAsync(message.Chat, GetRndUrlReply("Other"), ParseMode.Html, replyMarkup: GetRegionButtons());
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
            ){ ResizeKeyboard = true };
        }

        private static IReplyMarkup GetRegionButtons()
        {
            ReplyKeyboardMarkup keyboard = new(new[]
                                                    {
                                                        new KeyboardButton[] {"Англия", "Русь", "Франция" },
                                                        new KeyboardButton[] { "Германия", "Скандинавия", "Ацтеки" },
                                                        new KeyboardButton[] { "Иерусалим", "Славянские народы", "Шотландия" },
                                                        new KeyboardButton[] { "Испания", "Прочие", "Меню" }
                                                    }
                                              ){ ResizeKeyboard = true };
            return keyboard;
            //return new ReplyKeyboardMarkup
            //(
            //    new List<List<KeyboardButton>>
            //    {
            //        new List<KeyboardButton>{ new KeyboardButton ("Англия"), new KeyboardButton ("Русь"), new KeyboardButton("Франция"), new KeyboardButton("Германия"),
            //                                  new KeyboardButton("Скандинавия"), new KeyboardButton ("Ацтеки"), new KeyboardButton ("Иерусалим"), new KeyboardButton ("Славянские народы"),
            //                                  new KeyboardButton ("Шотландия"), new KeyboardButton ("Испания"), new KeyboardButton ("Прочие"), new KeyboardButton ("Меню")}
            //    }
            //)
            //{ResizeKeyboard = true };
        }

        private string GetRndUrlReply(string Name)
        {
            string error = "Данные не найдены";

            XmlHistoryList historyList = new XmlHistoryList(Name);

            if (historyList.Links != null && historyList.Links.Count > 0)
            {
                int rndValue = rnd.Next(0, historyList.Links.Count - 1);

                var reply = GetUrlReply(historyList.Links[rndValue].Url, historyList.Links[rndValue].Name);

                if (!string.IsNullOrEmpty(reply))
                    return reply;
                else
                    return error;
            }
            else
            {
                return error;
            }
        }

        private string GetUrlReply(string url, string text)
        {
            if (string.IsNullOrEmpty(url)) return null;

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
