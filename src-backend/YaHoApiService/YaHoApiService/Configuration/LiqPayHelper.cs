using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YaHo.YaHoApiService.Common.Helpers;

namespace YaHo.YaHoApiService.Configuration
{
    public class LiqPayHelper
    {
        private const string PrivateKey = "sandbox_jzvr1lJZ6kJON5X4C0XUQHWBtsNOciLVRB5lD4hp";
        private const string PublicKey = "sandbox_i10088105046";

        public static async Task<(string, string)> GetLiqPayProcessedData(decimal amount, string orderId, string resultUrl)
        {
            var signature = new LiqPayData
            {
                public_key = PublicKey,
                version = 3,
                action = "pay",
                amount = amount,
                currency = "UAH",
                description = "Purchasing ",
                order_id = orderId,
                sandbox = 1,

                result_url = resultUrl,
                server_url = "https://localhost:44360/api/Account/liq-pay-result/",
                info = "Some info about purchasing....",
                product_category = "Delivery payment",
                product_description = "Delivery insurance",
                product_name = "Universe staff"
            };

            var json = JsonConvert.SerializeObject(signature);
            var dataHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            var signatureHash = GetLiqPaySignature(dataHash);

            return (dataHash, signatureHash);
        }

        public static string GetLiqPaySignature(string data)
        {
            var key = PrivateKey + data + PrivateKey;
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var hash = SHA1.Create().ComputeHash(keyBytes);

            return Convert.ToBase64String(hash);
        }
    }
}
