using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class LiqPayOrderConfiguration : IEntityTypeConfiguration<LiqPayOrderDbo>
    {
        public void Configure(EntityTypeBuilder<LiqPayOrderDbo> builder)
        {
            builder.ToTable(DataBaseTables.LiqPayOrders);

            builder.HasKey(x => x.LiqPayOrderId);

            builder.Property(x => x.InitialDate);

            builder.Property(x => x.Money)
                .HasColumnType("decimal(18,1)");

            builder.HasIndex(x => x.UserId);

            builder.HasOne(x => x.User)
                .WithMany(r => r.LiqPayOrders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
