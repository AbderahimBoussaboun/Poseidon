using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.Entities.ResourceMaps.Servers;

namespace Poseidon.Repositories.ResourceMaps.Configurations.Servers
{
    public class ServerConfiguration : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            //Primary Key
            builder.HasKey(s => s.Id);

            //Properties
            builder.Property(s => s.Id).HasDefaultValueSql("NEWID()").HasColumnName("ServerId"); 
            builder.Property(s => s.Name).IsRequired();

            //Foreign Keys
            builder.HasOne(s => s.Environment).WithMany(s => s.Servers).HasForeignKey(s => s.EnvironmentId);
            builder.HasOne(s => s.Product).WithMany(s => s.Servers).HasForeignKey(s => s.ProductId);
        }
    }
}
