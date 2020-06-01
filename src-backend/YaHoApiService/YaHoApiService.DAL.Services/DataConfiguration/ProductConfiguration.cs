using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductDbo>
    {
        public void Configure(EntityTypeBuilder<ProductDbo> builder)
        {
            builder.ToTable(DataBaseTables.Products);

            builder.HasKey(x => x.ProductId);

            builder.Property(x => x.Link)
                .HasMaxLength(300);

            builder.Property(x => x.ProductName)
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(300);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,1)");

            builder.Property(x => x.Tax)
                .HasColumnType("decimal(18,1)");

            builder.HasIndex(x => x.OrderId);

            builder.HasOne(x => x.Order)
                .WithMany(r => r.Products)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
