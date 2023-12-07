using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.Entities.ResourceMaps.Applications;

namespace Poseidon.Repositories.ResourceMaps.Configurations.Applications
{
    class SubApplicationConfiguration : IEntityTypeConfiguration<SubApplication>
    {
        public void Configure(EntityTypeBuilder<SubApplication> builder)
        {
            //Primary Key
            builder.HasKey(s => s.Id);

            //Properties
            builder.Property(s => s.Id).HasDefaultValueSql("NEWID()").HasColumnName("SubApplicationId"); 
            builder.Property(s => s.Name).IsRequired();

            //Foreign Keys
            builder.HasOne(s => s.Application).WithMany(a => a.SubAplications).HasForeignKey(a => a.ApplicationId);
        }
    }
}
