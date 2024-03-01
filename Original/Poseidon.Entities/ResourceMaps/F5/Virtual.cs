using System;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.F5 { 

public partial class Virtual: BaseEntity
{
    public Guid? PoolId { get; set; }

    public virtual Pool? Pool { get; set; }

    public virtual ICollection<Rule> Rules { get; set; } = new List<Rule>();
}
}
