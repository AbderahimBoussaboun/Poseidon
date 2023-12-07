using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.Entities.ResourceMaps.Servers;

namespace Poseidon.Repositories.ResourceMaps.Configurations.Servers
{
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //Primary Key
            builder.HasKey(r => r.Id);

            //Properties
            builder.Property(r => r.Id).HasDefaultValueSql("NEWID()").HasColumnName("RoleId"); 
            builder.Property(r => r.Name).IsRequired();

            //Foreign Keys
            builder.HasOne(r => r.Server).WithMany(r => r.Roles).HasForeignKey(r => r.ServerId);
        }
    }
}
