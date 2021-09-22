using System;
using System.Collections.Generic;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.OrderRequest;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.DAL.Data.Enums;
using YaHo.YaHoApiService.ViewModels.CustomerViewModels;
using YaHo.YaHoApiService.ViewModels.ProductViewModels;

namespace YaHo.YaHoApiService.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public DateTime InitialDate { get; set; }

        public string DeliveryPlace { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime ExpectedDate { get; set; }

        public int CustomerId { get; set; }

        public decimal DeliveryCharge { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public string DeliveryFrom { get; set; }

        public string OrderStatus { get; set; }

        public CustomerViewModel Customer { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public IEnumerable<OrderRequestViewData> OrderRequests { get; set; }
    }
}
