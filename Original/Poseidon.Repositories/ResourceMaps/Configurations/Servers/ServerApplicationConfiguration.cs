using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.Entities.ResourceMaps.Servers;

namespace Poseidon.Repositories.ResourceMaps.Configurations.Servers
{
    class ServerApplicationConfiguration : IEntityTypeConfiguration<ServerApplication>
    {
        public void Configure(EntityTypeBuilder<ServerApplication> builder)
        {
            //Primary Key
            builder.HasKey(ss => ss.Id);

            //Properties
            builder.Property(ss => ss.Id).HasDefaultValueSql("NEWID()").HasColumnName("ServerApplicationId"); 
            builder.Property(ss => ss.Name).IsRequired();

            //Foreign Keys
            builder.HasOne(ss => ss.Role).WithMany(a => a.ServerApplications).HasForeignKey(a => a.RoleId);
            builder.HasOne(ss => ss.Component).WithOne(c => c.ServerApplication);
        }
    }
}
