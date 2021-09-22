using System;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.LiqPayOrder
{
    public class LiqPayOrderViewData
    {
        public string LiqPayOrderId { get; set; }

        public string UserId { get; set; }

        public decimal Money { get; set; }

        public DateTime InitialDate { get; set; }

        public UserViewData User { get; set; }
    }
}
