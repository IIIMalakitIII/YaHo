using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace YaHo.YaHoApiService.ViewModels.MediaViewModels
{
    public class AddMediaToProduct
    {
        [Required]
        public int ProductId { get; set; }

        [DataType(DataType.Upload)]
        public List<IFormFile> Picture { get; set; }
    }
}
