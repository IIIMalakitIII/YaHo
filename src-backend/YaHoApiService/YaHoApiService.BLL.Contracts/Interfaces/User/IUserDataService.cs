using System.Collections.Generic;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;

namespace YaHo.YaHoApiService.BAL.Contracts.Interfaces.User
{
    public interface IUserDataService
    {
        Task<string> CreateUserAsync(CreateUserViewData model);

        Task<bool> IsUserEmailExistsAsync(string email);

        Task<bool> IsUserWithIdExistsAsync(string id);

        Task<bool> AnyUserWithThisTokenId(int tokenId);

        Task UpdateUserTelegramIdAsync(int telegramId, string userId);

        Task<List<UserViewData>> GetAllUserAsync();

        Task UpdateUserAsync(UpdateUserInfoViewData model);

        Task<string> GenerateTokenForUser(string email, string password);

        Task<UserViewData> GetUserAsync(string userId);

        Task ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task ReplenishUserBalanceAsync(string userId, decimal money);

        Task<bool> UserHasEnoughMoneyAsync(string id, decimal money);

        Task<bool> FreezeMoneyAsync(string userId, decimal money);

        Task<bool> DefrostMoneyAsync(string userId, decimal money);
    }
}
