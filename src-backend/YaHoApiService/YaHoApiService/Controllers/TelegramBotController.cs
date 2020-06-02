using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.Controllers;
using YaHoApiService.TelegramBot;

namespace YaHoApiService.Controllers
{
    [Route("/api/telegrams")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class TelegramBotController : Controller
    {
        private readonly IUserService _userService;

        public TelegramBotController(IMapper mapper, IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("df443335")]
        public async Task<StatusCodeResult> Post([FromBody] Update update)
        {
            if(update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                if(update?.Message?.Text?.Equals("/start") ?? false)
                {
                    var botClient = await Bot.GetBotClientAsync();
                    var message = update.Message;
                    long userId = message.From.Id;
                    if (_userService.TelegramIdExists(Convert.ToInt32(userId)).Result)
                    {
                        await botClient.SendTextMessageAsync(userId, "Ваш аккаунт подключен. Теперь в эту переписку вы будете получать информацию о новых предложениях к вашим заказам.");
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(userId, $"Ваш аккаунт неподключен. Подключите его в настройках вашего аккаунта.Ваш id:\n`{userId}`", parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                    }
                }
            }
            return Ok();
        }
    }
}