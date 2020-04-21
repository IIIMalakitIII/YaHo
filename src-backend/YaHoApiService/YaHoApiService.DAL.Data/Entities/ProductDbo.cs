using System.Collections.Generic;

namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class ProductDbo
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int Price { get; set; }

        public int Tax { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string ProductName { get; set; }

        public OrderDbo Order { get; set; }

        public ICollection<MediaDbo> Media { get; set; }
    }
}
