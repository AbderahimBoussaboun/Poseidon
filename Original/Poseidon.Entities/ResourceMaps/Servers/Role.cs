using Poseidon.Entities.ResourceMaps.Enums;
using System;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.Servers
{
    public class Role:BaseEntity
    {
        public Role() { }
        public TypeRole Type { get; set; }
        public Guid ServerId { get; set; }


        public virtual Server Server { get; set; }     
        public virtual ICollection<ServerApplication> ServerApplications{ get; set; }
    }
}
