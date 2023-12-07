using System;
using System.Collections.Generic;

namespace Poseidon.Api.Models.ResourceMaps.Requests.Balancers
{
    public class BalancerRequest : BaseRequest
    {
        public String Ip { get; set; }
        public List<String> Ports { get; set; }
    }
}
