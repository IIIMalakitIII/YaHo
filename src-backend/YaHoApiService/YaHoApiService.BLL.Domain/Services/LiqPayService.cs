using AutoMapper;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.LiqPayOrder;
using YaHo.YaHoApiService.BLL.Contracts.Interfaces.LiqPay;
using YaHo.YaHoApiService.BLL.Domain.Validations;
using YaHo.YaHoApiService.Common.Exceptions;
using YaHo.YaHoApiService.Common.Helpers;

namespace YaHo.YaHoApiService.BLL.Domain.Services
{
    public class LiqPayService : ILiqPayService
    {
        private readonly IUserDataService _userDataService;

        private readonly ILiqPayDataService _liqPayDataService;

        private readonly IMapper _mapper;

        private readonly UserValidator _userValidator;

        public LiqPayService(IUserDataService userDataService,
            ILiqPayDataService liqPayDataService, IMapper mapper)
        {
            _userDataService = userDataService;
            _mapper = mapper;
            _liqPayDataService = liqPayDataService;
            _userValidator = new UserValidator(userDataService);
        }

        public async Task<LiqPayDataViewData> CreateLiqPayOrder(decimal money, string userId)
        {
            await _userValidator.CheckUserWithThisIdExists(userId);

            var liqPayOrderId = await _liqPayDataService.CreateLiqPayOrderAsync(money, userId);

            if (string.IsNullOrEmpty(liqPayOrderId))
            {
                throw new BusinessLogicException("Something is wrong liqpay order not created");
            }

            var(dataHash, signatureHash) = LiqPayHelper.GetLiqPayProcessedData(money, liqPayOrderId, "http://localhost:5000/api/Account/liq-pay-result/");

            return new LiqPayDataViewData()
            {
                Money = money,
                Data = dataHash,
                Signature = signatureHash,
            };
        }

        public async Task LiqPayResult(string liqPayOrderId, decimal money)
        {
            var result = await _liqPayDataService.IsLiqPayOrderWithIdExistsAsync(liqPayOrderId);
            
            if (!result)
            {
                return;
            }

            var liqPayOrder = await _liqPayDataService.GetLiqPayOrderAsync(liqPayOrderId);

            await _userDataService.ReplenishUserBalanceAsync(liqPayOrder.UserId, money);
        }
    }
}
