using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class DeliveryReviewConfiguration : IEntityTypeConfiguration<DeliveryReviewDbo>
    {
        public void Configure(EntityTypeBuilder<DeliveryReviewDbo> builder)
        {
            builder.ToTable(DataBaseTables.DeliveryReviews);

            builder.HasKey(x => x.ReviewId);

            builder.Property(x => x.Description)
                .HasMaxLength(300);

            builder.Property(x => x.Mark);

            builder.HasIndex(x => x.DeliveryId);

            builder.HasIndex(x => x.UserId);

            builder.HasOne(x => x.Delivery)
                .WithMany(r => r.DeliveryReviews)
                .HasForeignKey(x => x.DeliveryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany(r => r.DeliveryReviews)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
