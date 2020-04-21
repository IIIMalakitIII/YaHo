using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderDbo>
    {
        public void Configure(EntityTypeBuilder<OrderDbo> builder)
        {
            builder.ToTable(DataBaseTables.Orders);

            builder.HasKey(x => x.OrderId);

            builder.Property(x => x.DeliveryPlace)
                .HasMaxLength(100);

            builder.Property(x => x.Title)
                .HasMaxLength(100);

            builder.Property(x => x.Comment)
                .HasMaxLength(300);

            builder.Property(x => x.DeliveryFrom)
                .HasMaxLength(100);

            builder.Property(x => x.InitialDate);

            builder.Property(x => x.DeliverDate);

            builder.Property(x => x.Bargain);

            builder.Property(x => x.ExpectedDate);

            builder.Property(x => x.ExpectedDateFault);

            builder.Property(x => x.OrderStatus);

            builder.HasIndex(x => x.CustomerId);

            builder.HasOne(x => x.Customer)
                .WithMany(r => r.Orders)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
