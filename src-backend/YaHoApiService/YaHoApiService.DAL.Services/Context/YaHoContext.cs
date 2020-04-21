using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Services.DataBuilders;

namespace YaHo.YaHoApiService.DAL.Services.Context
{
    public class YaHoContext : DbContext
    {
        public YaHoContext(DbContextOptions<YaHoContext> options) : base(options) { }

        public DbSet<UserDbo> Users { get; set; }

        public DbSet<OrderDbo> Orders { get; set; }

        public DbSet<CustomerDbo> Customers { get; set; }

        public DbSet<CustomerReviewDbo> CustomerReviews { get; set; }

        public DbSet<DeliveryDbo> Deliveries { get; set; }

        public DbSet<DeliveryReviewDbo> DeliveryReviews { get; set; }

        public DbSet<ProductDbo> Products { get; set; }

        public DbSet<MediaDbo> Media { get; set; }

        #region QueriesWithoutTracking

        public IQueryable<UserDbo> UsersWithoutTracking => Users.AsNoTracking();

        public IQueryable<OrderDbo> OrdersWithoutTracking => Orders.AsNoTracking();

        public IQueryable<CustomerDbo> CustomersWithoutTracking => Customers.AsNoTracking();

        public IQueryable<CustomerReviewDbo> CustomerReviewsWithoutTracking => CustomerReviews.AsNoTracking();

        public IQueryable<DeliveryDbo> DeliveriesWithoutTracking => Deliveries.AsNoTracking();

        public IQueryable<DeliveryReviewDbo> DeliveryReviewsWithoutTracking => DeliveryReviews.AsNoTracking();

        public IQueryable<ProductDbo> ProductsWithoutTracking => Products.AsNoTracking();

        public IQueryable<MediaDbo> MediaWithoutTracking => Media.AsNoTracking();

        #endregion

        public async Task<bool> TrySaveChangesAsync()
        {
            var affectedRows = await SaveChangesAsync().ConfigureAwait(false);

            return affectedRows > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(YaHoContext).Assembly);

            new UserDataBuilder(modelBuilder).SetData();
            new CustomerDataBuilder(modelBuilder).SetData();
            new DeliveryDataBuilder(modelBuilder).SetData();
            new OrderDataBuilder(modelBuilder).SetData();
            new ProductDataBuilder(modelBuilder).SetData();
        }
    }
}
