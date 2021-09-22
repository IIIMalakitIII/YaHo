using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Media;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Media;
using YaHo.YaHoApiService.Controllers;
using YaHo.YaHoApiService.ViewModels.MediaViewModels;

namespace YaHoApiService.Controllers
{
    [Route("api/Media")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MediaController : BaseApiController
    {
        private readonly IEnumerable<string> _extensions;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;

        public MediaController(IMapper mapper, IMediaService mediaService)
        {
            _mapper = mapper;
            _extensions = new[] { ".jpeg", ".bmp", ".png", ".jpg" };
            _mediaService = mediaService;
        }


        [HttpPost]
        [DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> MediaToProduct([FromForm] AddMediaToProduct model)
        {
            if (!FilesIsValid(model.Picture) && model.Picture != null)
            {
                return BadRequest("Files extension not allowed.");
            }

            var mediaViewData = new List<MediaViewData>();

            foreach (var file in model.Picture)
            {
                mediaViewData.Add(new MediaViewData()
                {
                    MediaId = 0,
                    ProductId = model.ProductId,
                    Picture = ConvertFileToBytes(file),
                    ContentType = file?.ContentType
                });
            }

            await _mediaService.AddMediaToProduct(mediaViewData, model.ProductId,  CurrentUser.CustomerId);

            return Ok();
        }

        [HttpDelete("{mediaId}/{productId}")]
        public async Task<IActionResult> Media(int mediaId, int productId)
        {
            await _mediaService.DeleteMedia(mediaId, productId,CurrentUser.CustomerId);

            return NoContent();
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