using System;
using System.Collections.Generic;
using System.Text;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;

namespace YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media
{
    public class MediaViewData
    {
        public int MediaId { get; set; }

        public int ProductId { get; set; }

        public string ContentType { get; set; }

        public byte[] Picture { get; set; }

    }
}
