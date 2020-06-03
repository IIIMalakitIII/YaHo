namespace YaHo.YaHoApiService.ViewModels.DeliveryViewModels
{
    public class GetDeliveryViewModel
    {
        public int DeliveryId { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public int TotalReviewCount { get; set; }
    }
}
