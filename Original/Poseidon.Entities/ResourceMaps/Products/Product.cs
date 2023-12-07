using Poseidon.Entities.ResourceMaps.Servers;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.Products
{
    public class Product : BaseEntity
    {
        public Product() { }
        public virtual ICollection<Applications.Application> Applications { get; set; }
        public virtual ICollection<Server> Servers { get; set; }
    }
}
