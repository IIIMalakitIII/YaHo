using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YaHo.YaHoApiService.DAL.Services.Context;

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
    }
}
