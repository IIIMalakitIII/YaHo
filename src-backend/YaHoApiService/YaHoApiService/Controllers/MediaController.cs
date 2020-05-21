using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.ViewModels.MediaViewModels;
using YaHo.YaHoApiService.ViewModels.ProductViewModels;

namespace YaHoApiService.Controllers
{
    [Route("api/Media")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MediaController : ControllerBase
    {
        private readonly IMapper _mapper;

        public MediaController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [HttpPost]
        [DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Media([FromForm] MediaViewModel model)
        {
            var mediaViewData = _mapper.Map<IEnumerable<MediaViewData>>(model);

            /*foreach (var media in mediaViewData.Zip(model, Tuple.Create))
            {
                media.Item1.Picture = ConvertFileToBytes(media.Item2.Picture);
                media.Item1.ContentType = media.Item2.Picture?.ContentType;
            }*/

            // await _productService.CreateProductAsync(createProduct);

            return Ok();
        }


        private byte[] ConvertFileToBytes(IFormFile file)
        {
            if (file is null)
            {
                return null;
            }

            using var ms = new MemoryStream();

            file.CopyTo(ms);

            return ms.ToArray();

        }
    }
}