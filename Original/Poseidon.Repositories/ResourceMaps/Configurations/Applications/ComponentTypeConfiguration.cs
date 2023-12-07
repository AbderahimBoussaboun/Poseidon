using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poseidon.Entities.ResourceMaps.Applications;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Repositories.ResourceMaps.Configurations.Servers
{
    class ComponentTypeConfiguration : IEntityTypeConfiguration<ComponentType>
    {
        public void Configure(EntityTypeBuilder<ComponentType> builder)
        {
            //Primary Key
            builder.HasKey(e => e.Id);

            //Properties
            builder.Property(e => e.Id).HasDefaultValueSql("NEWID()").HasColumnName("ComponentTypeId");
            builder.Property(e => e.Name).IsRequired();
        }
    }
}
