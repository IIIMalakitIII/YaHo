using System;
using System.Linq;
using YaHo.YaHoApiService.DAL.Data.Entities;

namespace YaHo.YaHoApiService.DAL.Services.QueryHelper
{

    public static class OrderQueryHelper
    {
        public static IQueryable<OrderDbo> FilterBy(this IQueryable<OrderDbo> query,
            string deliveryСountry,
            string deliveryCity,
            string deliveryAddress,
            string deliveryFromCity,
            string deliveryFromСountry,
            string filter,
            bool? bargain,
            DateTime? expectedDateFrom,
            DateTime? expectedDateTo)
        {
            if (!string.IsNullOrEmpty(deliveryСountry))
            {
                query = query.Where(x => x.DeliveryPlace.Contains(deliveryСountry));
            }

            if (!string.IsNullOrEmpty(deliveryCity))
            {
                query = query.Where(x => x.DeliveryPlace.Contains(deliveryCity));
            }

            if (!string.IsNullOrEmpty(deliveryAddress))
            {
                query = query.Where(x => x.DeliveryPlace.Contains(deliveryAddress));
            }

            if (!string.IsNullOrEmpty(deliveryFromСountry))
            {
                query = query.Where(x => x.DeliveryFrom.Contains(deliveryFromСountry));
            }

            if (!string.IsNullOrEmpty(deliveryFromCity))
            {
                query = query.Where(x => x.DeliveryFrom.Contains(deliveryFromCity));
            }

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x =>
                    x.Title.Contains(filter) ||
                    x.Comment.Contains(filter) ||
                    x.Products.All(x => x.ProductName.Contains(filter)) ||
                    x.Products.All(x => x.Description.Contains(filter)) ||
                    x.DeliveryFrom.Contains(filter) ||
                    x.DeliveryPlace.Contains(filter));
            }

            if (bargain.HasValue)
            {
                query = query.Where(x => x.Bargain == bargain);
            }

            if (expectedDateFrom.HasValue)
            {
                query = query.Where(x => x.ExpectedDate.Date >= expectedDateFrom.Value.Date);
            }

            if (expectedDateTo.HasValue)
            {
                query = query.Where(x => x.ExpectedDate.Date <= expectedDateTo.Value.Date);
            }

            if (!expectedDateFrom.HasValue)
            {
                query = query.Where(x => x.ExpectedDate.Date <= DateTime.UtcNow);
            }

            return query;
        }

    }
}
