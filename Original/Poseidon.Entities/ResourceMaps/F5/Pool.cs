using System;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.F5
{

    public partial class Pool : BaseEntity
    { 
        public Guid? MonitorId { get; set; }

       public string BalancerType { get; set; } = null!;

       public string? Description { get; set; }

        public virtual Monitor? Monitor { get; set; }

        public virtual ICollection<NodePool> NodePools { get; set; } = new List<NodePool>();

        public virtual ICollection<Virtual> Virtuals { get; set; } = new List<Virtual>();
    }
}