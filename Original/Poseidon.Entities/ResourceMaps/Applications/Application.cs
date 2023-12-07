using System;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.Applications
{
    public class Application:BaseEntity
    {
        public Application()
        {
        }

        public Guid ProductId { get; set; }

        public virtual Products.Product Product { get; set; }    
        public virtual ICollection<SubApplication> SubAplications { get; set; }
    }
}
