using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YaHo.YaHoApiService.DAL.Data.Entities;
using YaHo.YaHoApiService.DAL.Data.Tables;

namespace YaHo.YaHoApiService.DAL.Services.DataConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserDbo>
    {
        public void Configure(EntityTypeBuilder<UserDbo> builder)
        {
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(300);

            builder.Property(x => x.TelegramId);

            builder.Property(x => x.Balance)
                .HasColumnType("decimal(18,1)");

            builder.Property(x => x.Hold)
                .HasColumnType("decimal(18,1)");

            builder.Property(x => x.InitialDate)
                .IsRequired();

            builder.HasOne(x => x.Customer)
                .WithOne(r => r.User)
                .HasForeignKey<CustomerDbo>(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Delivery)
                .WithOne(r => r.User)
                .HasForeignKey<DeliveryDbo>(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
