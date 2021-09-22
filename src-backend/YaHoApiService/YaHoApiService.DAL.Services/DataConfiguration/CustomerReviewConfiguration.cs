using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class CustomerReviewConfiguration : IEntityTypeConfiguration<CustomerReviewDbo>
    {
        public void Configure(EntityTypeBuilder<CustomerReviewDbo> builder)
        {
            builder.ToTable(DataBaseTables.CustomerReviews);

            builder.HasKey(x => x.ReviewId);

            builder.Property(x => x.Description)
                .HasMaxLength(300);

            builder.Property(x => x.Mark);

            builder.HasIndex(x => x.CustomerId);

            builder.HasIndex(x => x.UserId);

            builder.HasOne(x => x.User)
                .WithMany(r => r.CustomerReviews)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                .WithMany(r => r.CustomerReviews)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
