using System;

namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class LiqPayOrderDbo
    {
        public string LiqPayOrderId { get; set; }

        public string UserId { get; set; }

        public decimal Money { get; set; }

        public DateTime InitialDate { get; set; }

        public UserDbo User { get; set; }
    }
}
