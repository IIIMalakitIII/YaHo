using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class OrderRequestConfiguration : IEntityTypeConfiguration<OrderRequestDbo>
    {
        public void Configure(EntityTypeBuilder<OrderRequestDbo> builder)
        {
            builder.ToTable(DataBaseTables.OrderRequests);

            builder.HasKey(x => x.OrderRequestId);

            builder.Property(x => x.Approved);

            builder.HasIndex(x => x.OrderId);

            builder.HasIndex(x => x.DeliveryId);

            builder.HasOne(x => x.Order)
                .WithMany(r => r.OrderRequests)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Delivery)
                .WithMany(r => r.OrderRequests)
                .HasForeignKey(x => x.DeliveryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
