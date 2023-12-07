using Poseidon.Entities.ResourceMaps.Balancers;
using Poseidon.Repositories.ResourceMaps.Interfaces.Balancers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Repositories.ResourceMaps.Repositories.Balancers
{
    public class BalancersRepository : GenericRepository, IBalancersRepository
    {

        public BalancersRepository(DataContext context) : base(context) { }

        Expression<Func<Balancer, object>>[] includesBalancer = new Expression<Func<Balancer, object>>[]
           {

           };

        public async Task<bool> DeleteBalancerEntity(Guid balancerId)
        {
            var result = await base.DeleteEntity<Balancer>(balancerId);
            return result;
        }

        public async Task<List<Balancer>> GetAllBalancers()
        {
            var result = await base.GetAll(includesBalancer);
            return result;
        }

        public async Task<Balancer> GetBalancerById(Guid balancerId)
        {
            var result = await base.GetById(balancerId, includesBalancer);
            return result;
        }

        public async Task<Guid> InsertBalancerEntity(Balancer balancer)
        {
            var result = await base.InsertEntity(balancer);
            return result;
        }

        public async Task<bool> UpdateBalancerEntity(Balancer balancer)
        {
            var result = await base.UpdateEntity(balancer);
            return result;
        }
    }
}
