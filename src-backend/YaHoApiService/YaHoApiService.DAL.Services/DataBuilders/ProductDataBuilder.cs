using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using YaHo.YaHoApiService.DAL.Data.Entities;

namespace YaHo.YaHoApiService.DAL.Services.DataBuilders
{
    class ProductDataBuilder : BaseDataBuilder
    {
        public ProductDataBuilder(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void SetData()
        {
            var products = new List<ProductDbo>
            {
                new ProductDbo
                {
                    ProductId = 1,
                    OrderId = 1,
                    Description = "Hello",
                    Price = 600,
                    Link = "Nothing",
                    ProductName = "PCR test",
                    Tax = 10
                },
                new ProductDbo
                {
                    ProductId = 2,
                    OrderId = 2,
                    Description = "Hello",
                    Price = 600,
                    Link = "Nothing",
                    ProductName = "PCR test",
                    Tax = 10
                },
                new ProductDbo
                {
                    ProductId = 3,
                    OrderId = 3,
                    Description = "Hello",
                    Price = 600,
                    Link = "Nothing",
                    ProductName = "PCR test",
                    Tax = 10
                },
                new ProductDbo
                {
                    ProductId = 4,
                    OrderId = 4,
                    Description = "Hello",
                    Price = 600,
                    Link = "Nothing",
                    ProductName = "PCR test",
                    Tax = 10
                },
                new ProductDbo
                {
                    ProductId = 5,
                    OrderId = 5,
                    Description = "Hello",
                    Price = 600,
                    Link = "Nothing",
                    ProductName = "PCR test",

                    Tax = 10
                },
                new ProductDbo
                {
                    ProductId = 6,
                    OrderId = 6,
                    Description = "Hello",
                    Price = 600,
                    Link = "Nothing",
                    ProductName = "PCR test",
                    Tax = 10
                },
                new ProductDbo
                {
                    ProductId = 7,
                    OrderId = 7,
                    Description = "Hello",
                    Price = 600,
                    Link = "Nothing",
                    ProductName = "PCR test",
                    Tax = 10
                },
                new ProductDbo
                {
                    ProductId = 8,
                    OrderId = 8,
                    Description = "Hello",
                    Price = 600,
                    Link = "Nothing",
                    ProductName = "PCR test",
                    Tax = 10
                },
                new ProductDbo
                {
                    ProductId = 9,
                    OrderId = 9,
                    Description = "Hello",
                    Price = 600,
                    Link = "Nothing",
                    ProductName = "PCR test",
                    Tax = 10
                }
            };

            ModelBuilder.Entity<ProductDbo>()
                .HasData(products);
        }
    }
}
