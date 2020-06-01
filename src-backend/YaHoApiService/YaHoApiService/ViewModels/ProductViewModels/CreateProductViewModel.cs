using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace YaHo.YaHoApiService.ViewModels.ProductViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Tax { get; set; }

        [Required]
        public string Description { get; set; }

        public string Link { get; set; }

        [Required]
        public string ProductName { get; set; }

        [DataType(DataType.Upload)]
        public List<IFormFile> Picture { get; set; }
    }
}
