using System;
using System.Collections.Generic;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Customer;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.OrderRequest;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.DAL.Data.Enums;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order
{
    public class OrderViewData
    {
        public int OrderId { get; set; }

        public DateTime? InitialDate { get; set; }

        public string DeliveryPlace { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public bool Bargain { get; set; }

        public DateTime ExpectedDate { get; set; }

        public int CustomerId { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public string DeliveryFrom { get; set; }

        public int DeliveryCharge { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        public DateTime ExpectedDateFault { get; set; }

        public CustomerViewData Customer { get; set; }

        public IEnumerable<ProductViewData> Products { get; set; }

        public IEnumerable<OrderRequestViewData> OrderRequests { get; set; }
        

    }
}
