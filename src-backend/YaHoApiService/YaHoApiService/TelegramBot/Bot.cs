using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace YaHoApiService.TelegramBot
{
    public static class Bot
    {
        private static TelegramBotClient botClient;
        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (botClient != null)
            {
                return botClient;
            }
            else
            {
                botClient = new TelegramBotClient(BotSettings.Token);
                await botClient.SetWebhookAsync(String.Concat(BotSettings.WebHookUrl, "/api/telegrams/df443335"));
            }
            return botClient;
        }
        public static async void SendNotification(int? userId)
        {
            if (userId != null)
            {
                await botClient.SendTextMessageAsync(userId, "У вас новое предложение к заказу!");
            }
        }

    }
}
