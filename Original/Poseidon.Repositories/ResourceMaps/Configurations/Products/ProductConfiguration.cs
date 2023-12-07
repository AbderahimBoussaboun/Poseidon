using Microsoft.EntityFrameworkCore;
using Poseidon.Entities.ResourceMaps.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Poseidon.Repositories.ResourceMaps.Configurations.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()").HasColumnName("ProductId");
            builder.Property(p => p.Name).IsRequired();
            
        }
    }
}
