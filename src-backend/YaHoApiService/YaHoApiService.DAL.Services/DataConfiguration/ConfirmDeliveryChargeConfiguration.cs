using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class ConfirmDeliveryChargeConfiguration : IEntityTypeConfiguration<ConfirmDeliveryChargeDbo>
    {
        public void Configure(EntityTypeBuilder<ConfirmDeliveryChargeDbo> builder)
        {
            builder.ToTable(DataBaseTables.ConfirmsDeliveryCharge);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DeliveryConfirm);

            builder.Property(x => x.CustomerConfirm);

            builder.Property(x => x.AutomaticConfirm);

            builder.Property(x => x.PreviousPrice)
                .HasColumnType("decimal(18,1)");

            builder.Property(x => x.NewPrice)
                .HasColumnType("decimal(18,1)");

            builder.Property(x => x.InitialDate);

            builder.HasIndex(x => x.OrderId);

            builder.HasOne(x => x.Order)
                .WithMany(r => r.ConfirmDeliveryCharges)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}