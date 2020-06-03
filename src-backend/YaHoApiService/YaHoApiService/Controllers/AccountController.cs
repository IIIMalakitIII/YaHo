using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;
using YaHo.YaHoApiService.BLL.Contracts.Interfaces.LiqPay;
using YaHo.YaHoApiService.Common.Helpers;
using YaHo.YaHoApiService.ViewModels.UserViewModels;
using YaHo.YaHoApiService.ViewModels.UserViewModels.Auth;
using YaHo.YaHoApiService.ViewModels.UserViewModels.Update;

namespace YaHo.YaHoApiService.Controllers
{
    [Route("api/Account")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class AccountController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILiqPayService _liqPayService;

        public AccountController(IMapper mapper, IUserService userService, ILiqPayService liqPayService)
        {
            _mapper = mapper;
            _userService = userService;
            _liqPayService = liqPayService;
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            await _userService.ChangePassword(CurrentUser.UserId, model.CurrentPassword, model.NewPassword);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(LogInViewModel model)
        {
            var token = await _userService.SignIn(model.Email, model.Password);

            return Ok(new { token });
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            var userViewData = _mapper.Map<CreateUserViewData>(model);

            await _userService.CreateUser(userViewData);

            return Ok();
        }

        [HttpPut("update-user")]
        public async Task<ActionResult> UpdateUser(UpdateUserInfoViewModel model)
        {
            var userViewData = _mapper.Map<UpdateUserInfoViewData>(model);

            userViewData.Id = CurrentUser.UserId;

            await _userService.UpdateUser(userViewData);

            return Ok();
        }


        [HttpPut("update-user-telegramId")]
        public async Task<ActionResult> UpdateUser(int telegramId)
        {
            await _userService.UpdateUserTelegramId(telegramId, CurrentUser.UserId);

            return Ok();
        }

        [HttpPost("go-to-liq-pay")]
        public async Task<ActionResult<LiqPayCheckoutFormModel>> GetLiqPayConfiguration(decimal money)
        {

            var liqPayDataViewData = await _liqPayService.CreateLiqPayOrder(money, CurrentUser.UserId);

            var liqPayDataViewModel = _mapper.Map<LiqPayCheckoutFormModel>(liqPayDataViewData);

            return Ok(liqPayDataViewModel);
        }

        [HttpPost("liq-pay-result")]
        public async Task<ActionResult> LiqPayResult()
        {

            var requestDictionary = Request.Form.Keys.ToDictionary(key => key, key => Request.Form[key]);

            var requestData = Convert.FromBase64String(requestDictionary["data"]);
            var decodedString = Encoding.UTF8.GetString(requestData);
            var requestDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedString);
            var mySignature = LiqPayHelper.GetLiqPaySignature(requestDictionary["data"]);

            if (requestDataDictionary["status"] == "sandbox")
            {
                if (!mySignature.Equals(requestDictionary["signature"]))
                {
                    return Redirect("http://localhost:4200/liq-pay-result/error");
                }

                var orderId = requestDataDictionary["order_id"];
                var money = Convert.ToDecimal(requestDataDictionary["amount"]);

                await _liqPayService.LiqPayResult(orderId, money);

                return Redirect("http://localhost:4200/liq-pay-result");
            }

            return Redirect("http://localhost:4200/liq-pay-result/error");

        }

    }
}