using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using YaHo.YaHoApiService.DAL.Data.Entities;

namespace YaHo.YaHoApiService.DAL.Services.DataBuilders
{
    class DeliveryDataBuilder : BaseDataBuilder
    {
        public DeliveryDataBuilder(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void SetData()
        {
            var deliveries = new List<DeliveryDbo>
            {
                new DeliveryDbo
                {
                    UserId = 1,
                    DeliveryId = 1,
                    Description = "Hello",
                    Rating = 0,
                },
                new DeliveryDbo
                {
                    UserId = 2,
                    DeliveryId = 2,
                    Description = "Hello",
                    Rating = 0,
                },
                new DeliveryDbo
                {
                    UserId = 3,
                    DeliveryId = 3,
                    Description = "Hello",
                    Rating = 0,
                },
                new DeliveryDbo
                {
                    UserId = 4,
                    DeliveryId = 4,
                    Description = "Hello",
                    Rating = 0,
                },
                new DeliveryDbo
                {
                    UserId = 5,
                    DeliveryId = 5,
                    Description = "Hello",
                    Rating = 0,
                },
                new DeliveryDbo
                {
                    UserId = 6,
                    DeliveryId = 6,
                    Description = "Hello",
                    Rating = 0,
                },
                new DeliveryDbo
                {
                    UserId = 7,
                    DeliveryId = 7,
                    Description = "Hello",
                    Rating = 0,
                },
                new DeliveryDbo
                {
                    UserId = 8,
                    DeliveryId = 8,
                    Description = "Hello",
                    Rating = 0,
                },
                new DeliveryDbo
                {
                    UserId = 9,
                    DeliveryId = 9,
                    Description = "Hello",
                    Rating = 0,
                }
            };

            ModelBuilder.Entity<DeliveryDbo>()
                .HasData(deliveries);
        }
    }
}
