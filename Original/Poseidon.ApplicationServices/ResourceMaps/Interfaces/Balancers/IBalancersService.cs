using Poseidon.Api.Models.ResourceMaps.Requests.Balancers;
using Poseidon.Entities.ResourceMaps.Balancers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Balancers
{
    public interface IBalancersService
    {
        public Task<List<Balancer>> GetBalancers();
        public Task<Balancer> GetBalancerById(Guid id);
        public Task<Guid> InsertBalancer(BalancerRequest balancer);
        public Task<bool> DeleteBalancer(Guid id);
        public Task<bool> UpdateBalancer(Guid balancerId, BalancerRequest balancer);
    }
}
