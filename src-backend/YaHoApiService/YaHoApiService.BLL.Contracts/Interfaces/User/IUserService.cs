using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.User
{
    public interface IUserService
    {
        Task CreateUser(CreateUserViewData model);

        Task<string> SignIn(string email, string password);

        Task UpdateUser(UpdateUserInfoViewData model);

        Task<UserViewData> GetUserById(string userId);

        Task<List<UserViewData>> GetAllUser();

        Task ChangePassword(string userId, string currentPassword, string newPassword);

        Task UpdateUserTelegramId(int telegramId, string userId);
    }
}
