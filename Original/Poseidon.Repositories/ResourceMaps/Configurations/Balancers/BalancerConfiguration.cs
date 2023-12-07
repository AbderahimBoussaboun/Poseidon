using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Poseidon.Entities.ResourceMaps.Balancers;
using System.Collections.Generic;

namespace Poseidon.Repositories.ResourceMaps.Configurations.Balancers
{
    class BalancerConfiguration : IEntityTypeConfiguration<Balancer>
    {
        public void Configure(EntityTypeBuilder<Balancer> builder)
        {
            //Primary Key
            builder.HasKey(b => b.Id);

            //Properties
            builder.Property(b => b.Id).HasDefaultValueSql("NEWID()").HasColumnName("BalancerId");
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Ports).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v));

          
        }
    }
}
