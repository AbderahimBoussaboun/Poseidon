using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Entities.ResourceMaps.F5
{
    public class Virtual : BaseEntity
    {
        public Virtual() { }

        public Guid PoolId { get; set; }
        public Pool Pool { get; set; }
        public List<Rule> Rules { get; set; }
    }
}
