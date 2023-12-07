using System;
using System.Collections.Generic;

namespace Poseidon.Api.Models.ResourceMaps.Requests.Applications
{
    public class ComponentRequest : BaseRequest
    {
        public String Ip { get; set; }
        public List<String> Ports { get; set; }
        public String QueryString { get; set; }
        public Guid SubApplicationId { get; set; }
        public Guid? BalancerId { get; set; }
        public Guid ServerId { get; set; }
        public Guid ComponentTypeId { get; set; }
    }
}
