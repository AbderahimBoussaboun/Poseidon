using System;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.F5
{

    public partial class Irule:BaseEntity
    { 
        public string? Redirect { get; set; }

        public Guid? RuleId { get; set; }

        public virtual Rule? Rule { get; set; }
    }
}