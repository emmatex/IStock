using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class StockStateConfiguration : IEntityTypeConfiguration<StockState>
    {
        public void Configure(EntityTypeBuilder<StockState> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(p => p.CreatedOn).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(128);
        }
    }
}
