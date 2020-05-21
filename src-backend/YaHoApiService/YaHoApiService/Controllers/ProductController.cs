using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.Common.Exceptions;
using YaHo.YaHoApiService.Controllers;
using YaHo.YaHoApiService.ViewModels.ProductViewModels;

namespace YaHoApiService.Controllers
{
    [Route("api/Products")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ProductController : BaseApiController
    {
        private readonly IEnumerable<string> _extensions;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _extensions = new[] {".jpeg", ".bmp", ".png", ".jpg"};
            _productService = productService;
        }


        [HttpPost]
        [DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Product([FromForm] CreateProductViewModel model)
        {
            if (!FilesIsValid(model.Picture))
            {
                return BadRequest("Files extension not allowed.");
            }

            var productViewData = _mapper.Map<ProductViewData>(model);
            productViewData.Media = new List<MediaViewData>();
            foreach (var file in model.Picture)
            {
                productViewData.Media.Append(new MediaViewData()
                {
                    MediaId = 0,
                    ProductId = 0,
                    Picture = ConvertFileToBytes(file),
                    ContentType = file?.ContentType
                });
            }

            await _productService.CreateProduct(productViewData, CurrentUser.CustomerId, CurrentUser.UserId);

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

        private bool FilesIsValid(IFormFileCollection files)
        {
            if (files is null)
            {
                return true;
            }

            foreach (var value in files)
            {
                if (!(value is IFormFile file))
                {
                    return false;
                }

                var extension = Path.GetExtension(file.FileName);

                if (!_extensions.Any(x => x.Equals(extension, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}