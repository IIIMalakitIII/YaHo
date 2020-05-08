using AutoMapper;

namespace YaHo.YaHoApiService.Mapping
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapMedia();

            MapOrderRequests();

            MapOrderss();

            MapProducts();

            MapDeliveryReviews();

            MapDeliveries();

            MapCustomerReviews();

            MapCustomers();

            MapUsers();
        }
    }
}
