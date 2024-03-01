using System;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.F5
{

    public partial class Node:BaseEntity
    { 
        public string Description { get; set; } = null!;

        public string Ip { get; set; } = null!;

        public virtual ICollection<NodePool> NodePools { get; set; } = new List<NodePool>();
    }
}