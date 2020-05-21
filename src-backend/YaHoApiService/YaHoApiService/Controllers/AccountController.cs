using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;
using YaHo.YaHoApiService.BLL.Contracts.ServiceResults.CreateResult;
using YaHo.YaHoApiService.ViewModels.UserViewModels.Auth;
using YaHo.YaHoApiService.ViewModels.UserViewModels.Update;

namespace YaHo.YaHoApiService.Controllers
{
    [Route("api/Account")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
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

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserInfoViewModel model)
        {
            var userViewData = _mapper.Map<UpdateUserInfoViewData>(model);

            await _userService.UpdateUser(userViewData);

            return Ok();
        }

    }
}