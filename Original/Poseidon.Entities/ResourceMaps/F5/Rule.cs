using System;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.F5
{

    public partial class Rule:BaseEntity 
    {
        public Guid? VirtualId { get; set; }

        public virtual ICollection<Irule> Irules { get; set; } = new List<Irule>();

        public virtual Virtual? Virtual { get; set; }
    }
}