using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using YaHo.YaHoApiService.Common.Authentication;
using YaHo.YaHoApiService.ViewModels.UserViewModels;

namespace YaHo.YaHoApiService.Configuration
{
    public static class AuthHelper
    {
        public static UserData GetUserData(ICollection<Claim> claims)
        {
            var userData = new UserData
            {
                UserId = GetClaimValue(claims, CustomClaimName.Id),
                LastName = GetClaimValue(claims, CustomClaimName.LastName),
                FirstName = GetClaimValue(claims, CustomClaimName.FirstName),
                Email = GetClaimValue(claims, CustomClaimName.Email),
                CustomerId = Convert.ToInt32(GetClaimValue(claims, CustomClaimName.Customer)),
                DeliveryId = Convert.ToInt32(GetClaimValue(claims, CustomClaimName.Delivery)),

            };

            return userData;
        }

        private static string GetClaimValue(IEnumerable<Claim> claims, string claimName)
        {
            return claims
                .FirstOrDefault(x => x.Type.Equals(claimName, StringComparison.InvariantCultureIgnoreCase))
                ?.Value;
        }
    }
}
