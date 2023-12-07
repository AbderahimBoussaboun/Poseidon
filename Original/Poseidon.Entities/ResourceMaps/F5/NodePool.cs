using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Entities.ResourceMaps.F5
{
    public class NodePool
    {
        public NodePool() { }
        public Guid NodeId { get; set; }
        public Guid PoolId { get; set; }
        public Node Node { get; set; }
        public Pool Pool { get; set; }
    }
}
