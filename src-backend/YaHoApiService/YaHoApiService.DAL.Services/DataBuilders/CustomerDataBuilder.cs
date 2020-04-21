using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using YaHo.YaHoApiService.DAL.Data.Entities;

namespace YaHo.YaHoApiService.DAL.Services.DataBuilders
{
    class CustomerDataBuilder : BaseDataBuilder
    {
        public CustomerDataBuilder(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void SetData()
        {
            var customers = new List<CustomerDbo>
            {
                new CustomerDbo
                {
                    UserId = 1,
                    CustomerId = 1,
                    Description = "Hello",
                    Rating = 0,
                },
                new CustomerDbo
                {
                    UserId = 2,
                    CustomerId = 2,
                    Description = "Hello",
                    Rating = 0,
                },
                new CustomerDbo
                {
                    UserId = 3,
                    CustomerId = 3,
                    Description = "Hello",
                    Rating = 0,
                },
                new CustomerDbo
                {
                    UserId = 4,
                    CustomerId = 4,
                    Description = "Hello",
                    Rating = 0,
                },
                new CustomerDbo
                {
                    UserId = 5,
                    CustomerId = 5,
                    Description = "Hello",
                    Rating = 0,
                },
                new CustomerDbo
                {
                    UserId = 6,
                    CustomerId = 6,
                    Description = "Hello",
                    Rating = 0,
                },
                new CustomerDbo
                {
                    UserId = 7,
                    CustomerId = 7,
                    Description = "Hello",
                    Rating = 0,
                },
                new CustomerDbo
                {
                    UserId = 8,
                    CustomerId = 8,
                    Description = "Hello",
                    Rating = 0,
                },
                new CustomerDbo
                {
                    UserId = 9,
                    CustomerId = 9,
                    Description = "Hello",
                    Rating = 0,
                }
            };

            ModelBuilder.Entity<CustomerDbo>()
                .HasData(customers);
        }
    }
}
