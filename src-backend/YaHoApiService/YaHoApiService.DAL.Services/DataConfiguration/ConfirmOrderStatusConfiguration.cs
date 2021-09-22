using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class ConfirmOrderStatusConfiguration : IEntityTypeConfiguration<ConfirmOrderStatusDbo>
    {
        public void Configure(EntityTypeBuilder<ConfirmOrderStatusDbo> builder)
        {
            builder.ToTable(DataBaseTables.ConfirmsOrderStatus);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DeliveryConfirm);

            builder.Property(x => x.CustomerConfirm);

            builder.Property(x => x.AutomaticConfirm);

            builder.Property(x => x.PreviousStatus);

            builder.Property(x => x.NewStatus);

            builder.Property(x => x.InitialDate);

            builder.HasIndex(x => x.CreaterId);

            builder.HasIndex(x => x.OrderId);

            builder.HasOne(x => x.Order)
                .WithMany(r => r.ConfirmsOrderStatus)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany(r => r.ConfirmsOrderStatus)
                .HasForeignKey(x => x.CreaterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
