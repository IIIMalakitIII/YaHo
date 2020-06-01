namespace YaHo.YaHoApiService.ViewModels.CustomerViewModels
{
    public class GetCustomerViewModel
    {
        public int CustomerId { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public int TotalReviewCount { get; set; }
    }
}
