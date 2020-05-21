using System.Threading.Tasks;
using AutoMapper;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.Common.EntityNames;
using YaHo.YaHoApiService.Common.Exceptions.DataLogic;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class ProductDataService : IProductDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public ProductDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateProductAsync(ProductViewData product)
        {
            var productDbo = _mapper.Map<ProductDbo>(product);

            _context.Products.Add(productDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.Product);
            }
        }
    }
}
