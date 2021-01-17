using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class StockReceiveConfiguration : IEntityTypeConfiguration<StockReceive>
    {
        public void Configure(EntityTypeBuilder<StockReceive> builder)
        {
            builder.Property(p => p.Id).IsRequired().HasMaxLength(128);
            builder.Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(p => p.CreatedOn).IsRequired();
            builder.Property(p => p.DocumentNo).IsRequired().HasMaxLength(50);
            builder.Property(p => p.CrossReference).IsRequired().HasMaxLength(128);
            builder.Property(p => p.DriverName).IsRequired().HasMaxLength(128);
            builder.Property(p => p.VehicleRegNo).HasMaxLength(50);
            builder.Property(p => p.ReceiverName).IsRequired().HasMaxLength(128);
        }
    }
}
