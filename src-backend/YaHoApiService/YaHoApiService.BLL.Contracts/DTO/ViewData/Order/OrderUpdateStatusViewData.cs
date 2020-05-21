using System;
using System.Collections.Generic;
using System.Text;
using YaHo.YaHoApiService.DAL.Data.Enums;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order
{
    public class OrderUpdateStatusViewData
    {
        public int OrderId { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
