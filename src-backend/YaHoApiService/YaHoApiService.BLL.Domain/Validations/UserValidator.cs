using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.Common.Exceptions.BusinessLogic;

namespace YaHo.YaHoApiService.BLL.Domain.Validations
{
    internal class UserValidator
    {
        private readonly IUserDataService _userDataService;

        public UserValidator(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }


        public async Task CheckUserEmailAlreadyExistsForCreate(string email)
        {
            if (await _userDataService.IsUserEmailExistsAsync(email))
            {
                throw new ValidationException($"A user with this email: '{email}'  already exists.");
            }
        }

        public async Task CheckUserHasEnoughMoney(string userId, int money)
        {
            if (!await _userDataService.UserHasEnoughMoneyAsync(userId, money))
            {
                throw new ValidationException($"You can’t place an order, you don’t have enough money");
            }
        }

        public async Task CheckUserEmailExists(string email)
        {
            if (!await _userDataService.IsUserEmailExistsAsync(email))
            {
                throw new ValidationException($"No user with this email: '{email}' .");
            }
        }

        public async Task CheckUserWithThisIdExists(string id)
        {
            if (!await _userDataService.IsUserWithIdExistsAsync(id))
            {
                throw new ValidationException($"No user with this id: '{id}'.");
            }
        }

    }
}
