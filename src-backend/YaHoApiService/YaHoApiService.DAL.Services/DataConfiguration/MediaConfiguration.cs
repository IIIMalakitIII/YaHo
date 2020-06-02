using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class MediaConfiguration : IEntityTypeConfiguration<MediaDbo>
    {
        public void Configure(EntityTypeBuilder<MediaDbo> builder)
        {
            builder.ToTable(DataBaseTables.Media);

            builder.HasKey(x => x.MediaId);

            builder.Property(x => x.Picture);

            builder.Property(x => x.ContentType)
                .HasMaxLength(100);

            builder.HasIndex(x => x.ProductId);

            builder.HasOne(x => x.Product)
                .WithMany(r => r.Media)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
