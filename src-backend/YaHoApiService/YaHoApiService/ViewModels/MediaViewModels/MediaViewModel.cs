using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using YaHo.YaHoApiService.Validators;

namespace YaHo.YaHoApiService.ViewModels.MediaViewModels
{
    public class MediaViewModel
    {
        public int MediaId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string ContentType { get; set; }

        [AllowedExtensions(new[] { ".jpeg", ".bmp", ".png", ".jpg" })]
        public IFormFileCollection Picture { get; set; }
    }
}
