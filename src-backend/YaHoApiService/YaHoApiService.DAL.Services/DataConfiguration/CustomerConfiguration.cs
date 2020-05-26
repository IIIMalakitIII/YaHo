using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerDbo>
    {
        public void Configure(EntityTypeBuilder<CustomerDbo> builder)
        {
            builder.ToTable(DataBaseTables.Customers);

            builder.HasKey(x => x.CustomerId);

            builder.Property(x => x.Description)
                .HasMaxLength(300);

            builder.Property(x => x.Rating);

            builder.Property(x => x.TotalRating);

            builder.HasIndex(x => x.UserId);

            builder.HasOne(x => x.User)
                .WithOne(r => r.Customer);
        }
    }
}
