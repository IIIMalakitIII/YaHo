using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Customer;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.Delivery;
using YaHo.YaHoApiService.BAL.Contracts.Interfaces.User;
using YaHo.YaHoApiService.BLL.Domain.Services;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Services.Context;
using YaHo.YaHoApiService.DAL.Services.DataServices;

namespace YaHoApiService.Configuration
{
    public static class ConfigurationHelper
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<YaHoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("YaHoDb"),
                    m => m.MigrationsAssembly(typeof(YaHoContext).Assembly.FullName)));
        }

        public static void ConfigureDiServices(this IServiceCollection services)
        {
            ConfigureDalServices(services);

            ConfigureBllServices(services);
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<UserDbo, IdentityRole>(options =>
                {
                    // Password
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    // User
                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters = null;
                })
                .AddEntityFrameworkStores<YaHoContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidIssuer = configuration["Auth:Issuer"],
                    ValidAudience = configuration["Auth:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Key"]))
                };
            });
        }

        private static void ConfigureBllServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IDeliveryService, DeliveryService>();
        }

        private static void ConfigureDalServices(IServiceCollection services)
        {
            services.AddScoped<IUserDataService, UserDataService>();
            services.AddScoped<ICustomerDataService, CustomerDataService>();
            services.AddScoped<IDeliveryDataService, DeliveryDataService>();
        }


        public static void MigrateDataBase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<YaHoContext>();
            dbContext.Database.Migrate();
        }
    }
}
