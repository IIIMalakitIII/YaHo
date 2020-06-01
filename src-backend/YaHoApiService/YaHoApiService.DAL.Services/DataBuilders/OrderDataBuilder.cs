using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Enums;

namespace YaHo.YaHoApiService.DAL.Services.DataBuilders
{
    class OrderDataBuilder : BaseDataBuilder
    {
        public OrderDataBuilder(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void SetData()
        {
            var orders = new List<OrderDbo>
            {
                new OrderDbo
                {
                    OrderId = 1,
                    DeliveryFrom = "USA, New York",
                    DeliveryPlace = "Ukraine, Kharkov",
                    Title = "PCR test",
                    Comment = "Hello",
                    OrderStatus = OrderStatus.InExpectation,
                    CustomerId = 1,
                    ExpectedDate = DateTime.UtcNow.AddDays(5),
                    InitialDate = DateTime.UtcNow.AddHours(-2)
                },
                new OrderDbo
                {
                    OrderId = 2,
                    DeliveryFrom = "USA, New York",
                    DeliveryPlace = "Ukraine, Kharkov",
                    Title = "PCR test",
                    Comment = "Hello",
                    OrderStatus = OrderStatus.InExpectation,
                    CustomerId = 2,
                    ExpectedDate = DateTime.UtcNow.AddDays(5),
                    InitialDate = DateTime.UtcNow.AddHours(-3)
                },
                new OrderDbo
                {
                    OrderId = 3,
                    DeliveryFrom = "USA, New York",
                    DeliveryPlace = "Ukraine, Kharkov",
                    Title = "PCR test",
                    Comment = "Hello",
                    OrderStatus = OrderStatus.InExpectation,
                    CustomerId = 3,
                    ExpectedDate = DateTime.UtcNow.AddDays(5),
                    InitialDate = DateTime.UtcNow.AddHours(-4)
                },
                new OrderDbo
                {
                    OrderId = 5,
                    DeliveryFrom = "USA, New York",
                    DeliveryPlace = "Ukraine, Kharkov",
                    Title = "PCR test",
                    Comment = "Hello",
                    OrderStatus = OrderStatus.InExpectation,
                    CustomerId = 4,
                    ExpectedDate = DateTime.UtcNow.AddDays(5),
                    InitialDate = DateTime.UtcNow.AddHours(-5)
                },
                new OrderDbo
                {
                    OrderId = 4,
                    DeliveryFrom = "USA, New York",
                    DeliveryPlace = "Ukraine, Kharkov",
                    Title = "PCR test",
                    Comment = "Hello",
                    OrderStatus = OrderStatus.InExpectation,
                    CustomerId = 4,
                    ExpectedDate = DateTime.UtcNow.AddDays(5),
                    InitialDate = DateTime.UtcNow.AddHours(-6)
                },
                new OrderDbo
                {
                    OrderId = 6,
                    DeliveryFrom = "USA, New York",
                    DeliveryPlace = "Ukraine, Kharkov",
                    Title = "PCR test",
                    Comment = "Hello",
                    OrderStatus = OrderStatus.InExpectation,
                    CustomerId = 6,
                    ExpectedDate = DateTime.UtcNow.AddDays(5),
                    InitialDate = DateTime.UtcNow.AddHours(-7)
                },
                new OrderDbo
                {
                    OrderId = 7,
                    DeliveryFrom = "USA, New York",
                    DeliveryPlace = "Ukraine, Kharkov",
                    Title = "PCR test",
                    Comment = "Hello",
                    OrderStatus = OrderStatus.InExpectation,
                    CustomerId = 7,
                    ExpectedDate = DateTime.UtcNow.AddDays(5),
                    InitialDate = DateTime.UtcNow.AddHours(-8)
                },
                new OrderDbo
                {
                    OrderId = 8,
                    DeliveryFrom = "USA, New York",
                    DeliveryPlace = "Ukraine, Kharkov",
                    Title = "PCR test",
                    Comment = "Hello",
                    OrderStatus = OrderStatus.InExpectation,
                    CustomerId = 8,
                    ExpectedDate = DateTime.UtcNow.AddDays(5),
                    InitialDate = DateTime.UtcNow.AddHours(-2)
                },
                new OrderDbo
                {
                    OrderId = 9,
                    DeliveryFrom = "USA, New York",
                    DeliveryPlace = "Ukraine, Kharkov",
                    Title = "PCR test",
                    Comment = "Hello",
                    OrderStatus = OrderStatus.InExpectation,
                    CustomerId = 9,
                    ExpectedDate = DateTime.UtcNow.AddDays(5),
                    InitialDate = DateTime.UtcNow.AddHours(-2)
                }
            };

            ModelBuilder.Entity<OrderDbo>()
                .HasData(orders);
        }
    }
}
