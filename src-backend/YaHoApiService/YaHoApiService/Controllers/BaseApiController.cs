using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using YaHo.YaHoApiService.Configuration;
using YaHo.YaHoApiService.ViewModels.UserViewModels;

namespace YaHo.YaHoApiService.Controllers
{
    public class BaseApiController : Controller
    {
        protected UserData CurrentUser;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            CurrentUser = AuthHelper.GetUserData(HttpContext.User.Claims.ToList());
        }
    }
}