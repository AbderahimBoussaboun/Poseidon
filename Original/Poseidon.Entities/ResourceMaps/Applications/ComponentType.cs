using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.Applications
{
    public class ComponentType : BaseEntity
    {
        public ComponentType() { }
        public virtual ICollection<Component> Components { get; set; }
    }
}
