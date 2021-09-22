using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaHo.Common.Helpers;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Product;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.Product.Update;
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

        public async Task<bool> CheckCustomerHaveAccess(int productId, int customerId)
        {
            return await _context.ProductsWithoutTracking
                .Include(x => x.Order)
                .Where( x => x.ProductId == productId)
                .AnyAsync(x => x.Order.CustomerId == customerId);
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

        public async Task<List<ProductViewData>> GetProductsByOrderIdAsync(int orderId)
        {
            var productsDbo = await _context.ProductsWithoutTracking
                .Include(x => x.Media)
                .Where(x => x.OrderId == orderId)
                .ToListAsync();

            var productsViewData = _mapper.Map<List<ProductViewData>>(productsDbo);

            return productsViewData;
        }

        public async Task<ProductViewData> GetProductByIdAsync(int productId)
        {
            var productDbo = await _context.ProductsWithoutTracking
                .Include(x => x.Media)
                .Where(x => x.ProductId == productId)
                .FirstOrDefaultAsync();

            var productViewData = _mapper.Map<ProductViewData>(productDbo);

            return productViewData;
        }

        public async Task<bool> IsProductWithIdExistsAsync(int productId)
        {
            return await _context.ProductsWithoutTracking.AnyAsync(x => x.ProductId == productId);
        }

        public async Task UpdateProductInfoAsync(UpdateProductViewData model)
        {
            var productDbo = await _context.Products
                .FirstOrDefaultAsync(x => x.ProductId == model.ProductId);

            ObjectValidationHelper.CheckObjectNotNull(productDbo, EntityNames.Product, model.ProductId);

            _mapper.Map(model, productDbo);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductPriceAsync(int projectId, decimal newPrice, decimal newTax)
        {
            var productDbo = await _context.Products
                .FirstOrDefaultAsync(x => x.ProductId == projectId);

            ObjectValidationHelper.CheckObjectNotNull(productDbo, EntityNames.Product, projectId);

            productDbo.Price = newPrice;
            productDbo.Tax = newTax;

            _context.Products.Update(productDbo);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int projectId)
        {
            var productDbo = await _context.Products
                .FirstOrDefaultAsync(x => x.ProductId == projectId);

            ObjectValidationHelper.CheckObjectNotNull(productDbo, EntityNames.Product, projectId);

            _context.Products.Remove(productDbo);

            await _context.SaveChangesAsync();
        }
    }
}
