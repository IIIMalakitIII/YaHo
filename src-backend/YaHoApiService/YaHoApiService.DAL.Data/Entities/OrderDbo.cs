using System;
using System.Collections.Generic;
using YaHo.YaHoApiService.DAL.Data.Enums;

namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class OrderDbo
    {
        public int OrderId { get; set; }

        public DateTime InitialDate { get; set; }

        public string DeliveryPlace { get; set; }

        public DateTime? DeliverDate { get; set; }

        public bool Bargain { get; set; }

        public DateTime ExpectedDate { get; set; }

        public int CustomerId { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public string DeliveryFrom { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        public DateTime ExpectedDateFault { get; set; }

        public CustomerDbo Customer { get; set; }

        public ICollection<ProductDbo> Products { get; set; }

        public ICollection<OrderRequestDbo> OrderRequests { get; set; }


    }
}
