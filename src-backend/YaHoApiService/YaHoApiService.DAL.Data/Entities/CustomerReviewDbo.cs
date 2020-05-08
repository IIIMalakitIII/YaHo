namespace YaHo.YaHoApiService.DAL.Data.Entities
{
    public class CustomerReviewDbo
    {
        public int ReviewId { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public int CustomerId { get; set; }

        public int? Mark { get; set; }

        public UserDbo User { get; set; }

        public CustomerDbo Customer { get; set; }
    }
}
