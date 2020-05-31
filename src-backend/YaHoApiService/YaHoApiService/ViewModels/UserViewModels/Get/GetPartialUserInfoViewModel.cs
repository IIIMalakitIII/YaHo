using System;
using YaHo.YaHoApiService.ViewModels.CustomerViewModels;
using YaHo.YaHoApiService.ViewModels.DeliveryViewModels;

namespace YaHo.YaHoApiService.ViewModels.UserViewModels.Get
{
    public class GetPartialUserInfoViewModel : UserViewModel
    {
        public DateTime? InitialDate { get; set; }

        public GetCustomerViewModel Customer { get; set; }

        public GetDeliveryViewModel Delivery { get; set; }
    }
}
