using Poseidon.Entities.ResourceMaps.Balancers;
using System.Collections.Generic;

namespace Poseidon.Api.Models.ResourceMaps.Responses.Balancers
{
    public class GetAllBalancersResponse
    {
        public List<Balancer> Balancers { get; set; }
    }
}
