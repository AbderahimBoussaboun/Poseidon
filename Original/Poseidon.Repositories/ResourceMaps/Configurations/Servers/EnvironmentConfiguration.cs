using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.Entities.ResourceMaps.Servers;

namespace Poseidon.Repositories.ResourceMaps.Configurations.Servers
{
    class EnvironmentConfiguration : IEntityTypeConfiguration<Environment>
    {
        public void Configure(EntityTypeBuilder<Environment> builder)
        {
            //Primary Key
            builder.HasKey(e => e.Id);

            //Properties
            builder.Property(e => e.Id).HasDefaultValueSql("NEWID()").HasColumnName("EnvironmentId"); 
            builder.Property(e => e.Name).IsRequired();          
        }
    }
}
