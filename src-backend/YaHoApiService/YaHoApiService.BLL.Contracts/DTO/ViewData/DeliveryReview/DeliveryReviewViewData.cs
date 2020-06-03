using System;
using System.Collections.Generic;
using System.Text;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Delivery;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.DeliveryReview
{
    public class DeliveryReviewViewData
    {
        public int ReviewId { get; set; }

        public string Description { get; set; }

        public int DeliveryId { get; set; }

        public string UserId { get; set; }

        public int Mark { get; set; }

        public UserViewData User { get; set; }

        public DeliveryViewData Delivery { get; set; }
    }
}
