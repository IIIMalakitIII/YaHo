using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product.Update;
using YaHo.YaHoApiService.Controllers;
using YaHo.YaHoApiService.ViewModels.ProductViewModels;
using YaHo.YaHoApiService.ViewModels.ProductViewModels.Update;

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
            if (model.Picture != null)
            {
                productViewData.Media = new List<MediaViewData>();
                foreach (var file in model.Picture)
                {
                    productViewData.Media.Add(new MediaViewData()
                    {
                        MediaId = 0,
                        ProductId = 0,
                        Picture = ConvertFileToBytes(file),
                        ContentType = file?.ContentType
                    });
                }
            }

            await _productService.CreateProduct(productViewData, CurrentUser.CustomerId, CurrentUser.UserId);

            return Ok();
        }

        [HttpGet("products-by-order-id")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<IEnumerable<GetProductViewModel>>> ProductsByOrderId(int orderId)
        {
            var productsViewData = await _productService.GetProductsByOrderId(orderId, CurrentUser.UserId);
            var productsViewModel = _mapper.Map<IEnumerable<GetProductViewModel>>(productsViewData);

            return Ok(productsViewModel);
        }


        [HttpGet("product-by-id")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<GetProductViewModel>> ProductById(int productId)
        {
            var productViewData = await _productService.GetProductById(productId, CurrentUser.UserId);
            var productViewModel = _mapper.Map<GetProductViewModel>(productViewData);

            return Ok(productViewModel);
        }


        [HttpPut("product-by-id")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> ProductInfo(UpdateProductViewModel model)
        {
            var productViewData = _mapper.Map<UpdateProductViewData>(model);
            await _productService.UpdateProductInfo(productViewData, CurrentUser.CustomerId);

            return Ok();
        }

        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productService.DeleteProduct(productId, CurrentUser.CustomerId, CurrentUser.UserId);
            return Ok();
        }


        #region Private_method

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

        private bool FilesIsValid(List<IFormFile> files)
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

        #endregion
    }
}