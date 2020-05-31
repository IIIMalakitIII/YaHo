using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;
using YaHo.YaHoApiService.Configuration;
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

        public AccountController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            await _userService.ChangePassword(model.Id, model.CurrentPassword, model.NewPassword);

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


        [HttpGet("go-to-liq-pay")]
        public async Task<ActionResult> GetLiqPayConfiguration(decimal money)
        {
            var (dataHash, signatureHash) = await
                LiqPayHelper.GetLiqPayProcessedData(money, Guid.NewGuid().ToString(), "http://localhost:4200");

            return Ok(new
            {
                Money = money,
                Data = dataHash,
                Signature = signatureHash,
            });
        }

        [HttpPost("liq-pay-result")]
        public IActionResult LiqPayResult()
        {
            var requestDictionary = Request.Form.Keys.ToDictionary(key => key, key => Request.Form[key]);

            var requestData = Convert.FromBase64String(requestDictionary["data"]);
            var decodedString = Encoding.UTF8.GetString(requestData);
            var requestDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedString);
            var mySignature = LiqPayHelper.GetLiqPaySignature(requestDictionary["data"]);

            if (!mySignature.Equals(requestDictionary["signature"]))
            {
                //return RedirectToAction("Completed", new { success = false, result = "ERROR" });
            }

            var orderId = requestDataDictionary["order_id"];
            var transactionId = requestDataDictionary["transaction_id"];

            //return RedirectToAction("Completed", new { success = true, result = "Success", orderId, transactionId });
            return Ok();
        }

    }
}