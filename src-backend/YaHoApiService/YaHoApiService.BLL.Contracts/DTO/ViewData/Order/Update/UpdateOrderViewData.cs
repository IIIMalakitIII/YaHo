namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Order.Update
{
    public class UpdateOrderViewData
    {
        public int OrderId { get; set; }

        public string DeliveryPlace { get; set; }

        public bool Bargain { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public string DeliveryFrom { get; set; }

    }
}
