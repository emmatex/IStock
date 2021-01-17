using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class StockReceiveDetailConfiguration : IEntityTypeConfiguration<StockReceiveDetail>
    {
        public void Configure(EntityTypeBuilder<StockReceiveDetail> builder)
        {
            builder.Property(p => p.Id).IsRequired().HasMaxLength(128);
            builder.Property(p => p.CreatedOn).IsRequired().HasMaxLength(128);
            builder.Property(p => p.ProductCode).IsRequired().HasMaxLength(128);
            builder.Property(p => p.ProductName).IsRequired().HasMaxLength(128);
            builder.Property(p => p.Measurement).IsRequired().HasMaxLength(128);
            builder.Property(p => p.StockStateName).IsRequired().HasMaxLength(128);
            builder.Property(p => p.Remark);

            builder.HasOne(b => b.Product).WithMany()
               .HasForeignKey(p => p.ProductId);
            builder.HasOne(t => t.StockReceive).WithMany()
                .HasForeignKey(p => p.StockReceiveId);
            builder.HasOne(t => t.StockState).WithMany()
               .HasForeignKey(p => p.StockStateId);
        }
    }
}
