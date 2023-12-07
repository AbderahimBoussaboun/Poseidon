using System;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.Balancers
{
    public class Balancer:BaseEntity
    {
        public Balancer() { }
        public String Ip { get; set; }
        public List<String> Ports { get; set; }

        public virtual ICollection<Applications.Component> Components { get; set; }
    }
}
