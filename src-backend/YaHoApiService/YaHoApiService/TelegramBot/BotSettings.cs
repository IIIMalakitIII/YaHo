using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaHoApiService.TelegramBot
{
    public class BotSettings
    {
        public static string Name { get; set; } = "YaHo_bot";
        public static string Token { get; set; } = "970880534:AAE2Pzz4IHZC2QPjJg4QSQrUND7-MsJHcDw";    // Bot token that gives botfather
        public static string WebHookUrl { get; set; } = "https://00761eb883d1.ngrok.io";    // Part of webhook url that gives ngrok. Command to get it for ssl in IIS Express: ngrok http https://localhost:44360 -host-header="localhost:44360"
                                                                                       // ngrok http http://localhost:44359 -host-header="localhost:44359"
    }
}
