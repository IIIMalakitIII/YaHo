namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class OrderRequestDbo
    {
        public int OrderRequestId { get; set; }

        public int OrderId { get; set; }

        public int DeliveryId { get; set; }

        public bool Approved { get; set; }

        public OrderDbo Order { get; set; }

        public DeliveryDbo Delivery { get; set; }

    }
}
