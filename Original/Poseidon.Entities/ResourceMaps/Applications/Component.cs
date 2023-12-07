using Poseidon.Entities.ResourceMaps.Balancers;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Collections.Generic;

namespace Poseidon.Entities.ResourceMaps.Applications
{
    public class Component : BaseEntity
    {

        public Component()
        {
        }
        public String Ip { get; set; }
        public List<String> Ports { get; set; }
        public String QueryString { get; set; }
        public Guid SubApplicationId { get; set; }
        public Guid? BalancerId { get; set; }
        public Guid ComponentTypeId { get; set; }
        public ServerApplication ServerApplication { get; set; }

        public virtual SubApplication SubApplication { get; set; }
        public virtual Balancer Balancer { get; set; }
        public virtual ComponentType ComponentType { get; set; }






    }
}
