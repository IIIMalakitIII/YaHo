using System;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Customer;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User
{
    public class UserViewData
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public int? Balance { get; set; }

        public int? Hold { get; set; }

        public DateTime? InitialDate { get; set; }

        public DeliveryViewData Delivery { get; set; }

        public CustomerViewData Customer { get; set; }

    }
}
