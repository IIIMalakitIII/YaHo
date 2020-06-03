using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.CustomerReview;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.CustomerReview;
using YaHo.YaHoApiService.Common.EntityNames;
using YaHo.YaHoApiService.Common.Exceptions.DataLogic;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class CustomerReviewDataService : ICustomerReviewDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;

        public CustomerReviewDataService(YaHoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddCustomerReviewAsync(CustomerReviewViewData model)
        {
            var customerReviewDbo = _mapper.Map<CustomerReviewDbo>(model);

            _context.CustomerReviews.Add(customerReviewDbo);

            var saved = await _context.TrySaveChangesAsync();

            if (!saved)
            {
                throw new CreateFailureException(EntityNames.CustomerReview);
            }
        }

        public async Task<double> CalculateCustomerRating(int customerId)
        {
            var rating = await _context.CustomerReviewsWithoutTracking
                .Where(x => x.CustomerId == customerId)
                .AverageAsync(x => x.Mark);

            return Math.Round(rating, 1);
        }

        public async Task<List<CustomerReviewViewData>> GetCustomerReviewAsync(int customerId)
        {
            var customerReviewsDbo = await _context.CustomerReviewsWithoutTracking
                .Include(x => x.User)
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();

            var customerReviewsViewData = _mapper.Map<List<CustomerReviewViewData>>(customerReviewsDbo);

            return customerReviewsViewData;
        }
    }
}
