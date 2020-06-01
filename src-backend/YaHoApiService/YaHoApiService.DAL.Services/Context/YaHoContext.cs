using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using YaHo.YaHoApiService.DAL.Data.Entities;

namespace YaHo.YaHoApiService.DAL.Services.Context
{
    public class YaHoContext : IdentityDbContext<UserDbo>
    {
        public YaHoContext(DbContextOptions<YaHoContext> options) : base(options) { }

        public DbSet<OrderDbo> Orders { get; set; }

        public DbSet<CustomerDbo> Customers { get; set; }

        public DbSet<CustomerReviewDbo> CustomerReviews { get; set; }

        public DbSet<DeliveryDbo> Deliveries { get; set; }

        public DbSet<LiqPayOrderDbo> LiqPayOrders { get; set; }

        public DbSet<ConfirmDeliveryChargeDbo> ConfirmsDeliveryCharge { get; set; }

        public DbSet<ConfirmExpectedDateDbo> ConfirmsExpectedDate { get; set; }

        public DbSet<ConfirmOrderStatusDbo> ConfirmsOrderStatus { get; set; }

        public DbSet<DeliveryReviewDbo> DeliveryReviews { get; set; }

        public DbSet<ProductDbo> Products { get; set; }

        public DbSet<MediaDbo> Media { get; set; }

        public DbSet<OrderRequestDbo> OrderRequests { get; set; }

        #region QueriesWithoutTracking

        public IQueryable<UserDbo> UsersWithoutTracking => Users.AsNoTracking();

        public IQueryable<OrderDbo> OrdersWithoutTracking => Orders.AsNoTracking();

        public IQueryable<CustomerDbo> CustomersWithoutTracking => Customers.AsNoTracking();

        public IQueryable<CustomerReviewDbo> CustomerReviewsWithoutTracking => CustomerReviews.AsNoTracking();

        public IQueryable<DeliveryDbo> DeliveriesWithoutTracking => Deliveries.AsNoTracking();

        public IQueryable<LiqPayOrderDbo> LiqPayOrdersWithoutTracking => LiqPayOrders.AsNoTracking();

        public IQueryable<ConfirmExpectedDateDbo> ConfirmsExpectedDateWithoutTracking => ConfirmsExpectedDate.AsNoTracking();

        public IQueryable<ConfirmDeliveryChargeDbo> ConfirmsDeliveryChargeWithoutTracking => ConfirmsDeliveryCharge.AsNoTracking();

        public IQueryable<ConfirmOrderStatusDbo> ConfirmsOrderStatusWithoutTracking => ConfirmsOrderStatus.AsNoTracking();

        public IQueryable<DeliveryReviewDbo> DeliveryReviewsWithoutTracking => DeliveryReviews.AsNoTracking();

        public IQueryable<ProductDbo> ProductsWithoutTracking => Products.AsNoTracking();

        public IQueryable<MediaDbo> MediaWithoutTracking => Media.AsNoTracking();

        public IQueryable<OrderRequestDbo> OrderRequestsWithoutTracking => OrderRequests.AsNoTracking();

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

            modelBuilder.Entity<UserDbo>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<LiqPayOrderDbo>()
                .Property(e => e.LiqPayOrderId)
                .ValueGeneratedOnAdd();

            //new UserDataBuilder(modelBuilder).SetData();
        }
    }
}
