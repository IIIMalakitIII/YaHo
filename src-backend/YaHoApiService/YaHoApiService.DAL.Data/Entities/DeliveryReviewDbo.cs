namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class DeliveryReviewDbo
    {
        public int ReviewId { get; set; }

        public string Description { get; set; }

        public int DeliveryId { get; set; }

        public int UserId { get; set; }

        public int? Mark { get; set; }

        public DeliveryDbo Delivery { get; set; }

        public UserDbo User { get; set; }


    }
}
