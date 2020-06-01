using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Contracts.DTO.ViewData.User;
using YaHo.YaHoApiService.Common.Authentication;
using YaHo.YaHoApiService.Common.EntityNames;
using YaHo.YaHoApiService.Common.Exceptions;
using YaHo.YaHoApiService.Common.Exceptions.DataLogic;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Services.Context;

namespace YaHo.YaHoApiService.DAL.Services.DataServices
{
    public class UserDataService : IUserDataService
    {
        private readonly YaHoContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<UserDbo> _userManager;
        private readonly JwtSettings _jwtSettings;

        public UserDataService(UserManager<UserDbo> userManager, YaHoContext context,
            IOptions<JwtSettings> jwtOptions,  IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<bool> IsUserEmailExistsAsync(string email)
        {
            return await _context.UsersWithoutTracking.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> UserHasEnoughMoneyAsync(string userId, decimal money)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user.Balance >= money;
        }

        public async Task<bool> IsUserWithIdExistsAsync(string id)
        {
            return await _context.UsersWithoutTracking.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> AnyUserWithThisTokenId(int tokenId)
        {
            return await _context.UsersWithoutTracking.AnyAsync(x => x.TelegramId == tokenId);
        }

        public async Task<bool> FreezeMoneyAsync(string userId, decimal money)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.Balance -= money;
            user.Hold += money;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> DefrostMoneyAsync(string userId, decimal money)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.Balance += money;
            user.Hold -= money;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<UserViewData> GetUserAsync(string userId)
        {
            var userDbo = await _context.UsersWithoutTracking
                .Include(x => x.Delivery)
                .Include(x => x.Customer)
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            var userViewData = _mapper.Map<UserViewData>(userDbo);

            return userViewData;
        }

        public async Task<List<UserViewData>> GetAllUserAsync()
        {
            var usersDbo = await _userManager.Users
                .Include(x => x.Customer)
                .Include(x => x.Delivery)
                .ToListAsync();

            var usersViewData = _mapper.Map<List<UserViewData>>(usersDbo);

            return usersViewData;
        }


        public async Task<string> CreateUserAsync(CreateUserViewData model)
        {
            var userDbo = _mapper.Map<UserDbo>(model);

            var result = await _userManager.CreateAsync(userDbo, model.Password);

            return result.Succeeded
                ? userDbo.Id
                : throw new CreateFailureException(EntityNames.User);
        }

        public async Task UpdateUserAsync(UpdateUserInfoViewData model)
        {
            var loadedDbo = await _userManager.FindByIdAsync(model.Id);

            _mapper.Map(model, loadedDbo);

            var result = await _userManager.UpdateAsync(loadedDbo);

            if (!result.Succeeded)
            {
                throw new CreateFailureException(EntityNames.User);
            }
        }

        public async Task UpdateUserTelegramIdAsync(int telegramId, string userId)
        {
            var loadedDbo = await _userManager.FindByIdAsync(userId);

            loadedDbo.TelegramId = telegramId;

            var result = await _userManager.UpdateAsync(loadedDbo);

            if (!result.Succeeded)
            {
                throw new CreateFailureException(EntityNames.User);
            }
        }

        public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!result.Succeeded)
            {
                throw new BusinessLogicException(string.Join("\n", result.Errors.Select(x => x.Description)));
            }
        }

        public async Task<string> GenerateTokenForUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null || (!await _userManager.CheckPasswordAsync(user, password)))
            {
                throw new BusinessLogicException("Email or password is incorrect.");
            }

            var token = await GenerateToken(user);

            return token;
        }

        public async Task ReplenishUserBalanceAsync(string userId, decimal money)
        {
            var loadedDbo = await _userManager.FindByIdAsync(userId);

            loadedDbo.Balance += money;

            var result = await _userManager.UpdateAsync(loadedDbo);

            if (!result.Succeeded)
            {
                throw new CreateFailureException(EntityNames.User);
            }
        }

        private async Task<string> GenerateToken(UserDbo user)
        {
            var claims = new List<Claim>();

            var (customer, delivery) = await GetUserParticipation(user.Id);

            claims.Add(new Claim(CustomClaimName.Id, user.Id));
            claims.Add(new Claim(CustomClaimName.Customer, customer));
            claims.Add(new Claim(CustomClaimName.Delivery, delivery));
            claims.Add(new Claim(CustomClaimName.Email, user.Email));
            claims.Add(new Claim(CustomClaimName.LastName, user.LastName));
            claims.Add(new Claim(CustomClaimName.FirstName, user.FirstName));

            var expires = DateTime.Now.AddHours(3);
            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

        private async Task<(string, string)> GetUserParticipation(string userId)
        {
            var user = await _context.UsersWithoutTracking
                .Include(x => x.Customer)
                .Include(x => x.Delivery)
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            return (user.Customer.CustomerId.ToString(), user.Delivery.DeliveryId.ToString());
        }
    }
}
