using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.Servers
{
    public class Environment:BaseEntity
    {
        public Environment() { }
        public virtual ICollection<Server> Servers { get; set; }

    }
}
