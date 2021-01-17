using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(p => p.Id).IsRequired().HasMaxLength(128);
            builder.Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(p => p.CreatedOn).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(128);           
        }
    }
}
