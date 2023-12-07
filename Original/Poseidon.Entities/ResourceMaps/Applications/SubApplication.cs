using System;
using System.Collections.Generic;


namespace Poseidon.Entities.ResourceMaps.Applications
{
    public class SubApplication:BaseEntity
    {

        public SubApplication() { }

        public Guid ApplicationId { get; set; }

        public virtual Application Application { get; set; }
        public virtual ICollection<Component> Components { get; set; }

    }
}
