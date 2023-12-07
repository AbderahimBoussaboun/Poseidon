using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.Entities.ResourceMaps.Applications;

namespace Poseidon.Repositories.ResourceMaps.Configurations.Applications
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            //Primary Key
            builder.HasKey(a => a.Id);

            //Properties
            builder.Property(a => a.Id).HasDefaultValueSql("NEWID()").HasColumnName("ApplicationId"); 
            builder.Property(a => a.Name).IsRequired();

            //Foreign Keys
            builder.HasOne(a => a.Product).WithMany(a => a.Applications).HasForeignKey(a => a.ProductId);
        }
    }
}
