using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.ViewModels.UserViewModels.Get;

namespace YaHo.YaHoApiService.Controllers
{
    [Route("api/UserInfo")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UserInfoController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserInfoController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<GetUserInfoViewModel>> UserInfo(string userId)
        {
            var userViewData = await _userService.GetUserById(userId);

            var userViewModels = _mapper.Map<GetUserInfoViewModel>(userViewData);

            return Ok(userViewModels);
        }

        [HttpGet("allUser")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetUserInfoViewModel>>> UsersInfo()
        {
            var userViewData = await _userService.GetAllUser();

            var userViewModels = _mapper.Map<IEnumerable<GetUserInfoViewModel>>(userViewData);

            return Ok(userViewModels);
        }

    }
}