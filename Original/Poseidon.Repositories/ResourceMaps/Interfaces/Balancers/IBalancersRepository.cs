using Poseidon.Entities.ResourceMaps.Balancers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Repositories.ResourceMaps.Interfaces.Balancers
{
    public interface IBalancersRepository
    {
        public Task<List<Balancer>> GetAllBalancers();
        public Task<Balancer> GetBalancerById(Guid balancerId);
        public Task<Guid> InsertBalancerEntity(Balancer balancer);
        public Task<bool> UpdateBalancerEntity(Balancer balancer);
        public Task<bool> DeleteBalancerEntity(Guid balancerId);
    }
}
