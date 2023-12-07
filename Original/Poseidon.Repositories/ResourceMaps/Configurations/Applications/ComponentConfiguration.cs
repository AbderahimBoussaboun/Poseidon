using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Poseidon.Entities.ResourceMaps.Applications;
using System.Collections.Generic;
using System.Data;
namespace Poseidon.Repositories.ResourceMaps.Configurations.Applications
{
    class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            //Primary Key
            builder.HasKey(c => c.Id);
            

            //Properties
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()").HasColumnName("ComponentId");
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Ports).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v));

            //Foreign Keys
            builder.HasOne(c => c.SubApplication).WithMany(c => c.Components).HasForeignKey(c => c.SubApplicationId).OnDelete(DeleteBehavior.NoAction); ;
            builder.HasOne(c => c.ComponentType).WithMany(c =>c.Components).HasForeignKey(c => c.ComponentTypeId);
            builder.HasOne(c => c.Balancer).WithMany(c => c.Components).HasForeignKey(c => c.BalancerId);


        }
    
    }
}
