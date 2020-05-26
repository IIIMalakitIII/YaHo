using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class DeliveryConfiguration : IEntityTypeConfiguration<DeliveryDbo>
    {
        public void Configure(EntityTypeBuilder<DeliveryDbo> builder)
        {
            builder.ToTable(DataBaseTables.Deliveries);

            builder.HasKey(x => x.DeliveryId);

            builder.Property(x => x.Description)
                .HasMaxLength(300);

            builder.Property(x => x.Rating);

            builder.Property(x => x.TotalRating);

            builder.HasIndex(x => x.UserId);

            builder.HasOne(x => x.User)
                .WithOne(y => y.Delivery);
        }
    }
}
