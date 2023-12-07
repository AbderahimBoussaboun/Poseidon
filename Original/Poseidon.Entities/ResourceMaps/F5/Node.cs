using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.Servers;

namespace Poseidon.Entities.ResourceMaps.F5
{
    public class Node : BaseEntity
    {
        public Node() { }
        public string IP { get; set; }
        public string Port { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Pool> Pools { get; set; }

    }
}
