using Poseidon.Api.Models.ResourceMaps.Requests.Balancers;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Balancers;
using Poseidon.Entities.ResourceMaps.Balancers;
using Poseidon.Repositories.ResourceMaps.Interfaces.Balancers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Balancers
{
    public class BalancersService : IBalancersService
    {
        private readonly IBalancersRepository _balancersRepository;

        public BalancersService(IBalancersRepository balancersRepository)
        {
            _balancersRepository = balancersRepository;
        }

        public async Task<bool> DeleteBalancer(Guid id)
        {
            var result = await _balancersRepository.DeleteBalancerEntity(id);
            return result;
        }

        public async Task<Balancer> GetBalancerById(Guid id)
        {
            var result = await _balancersRepository.GetBalancerById(id);
            return result;
        }

        public async Task<List<Balancer>> GetBalancers()
        {
            var result = await _balancersRepository.GetAllBalancers();
            return result;
        }

        public async Task<Guid> InsertBalancer(BalancerRequest balancer)
        {
            var temp = new Balancer() { Name = balancer.Name,Ip=balancer.Ip,Ports=balancer.Ports, Active = balancer.Active };
            var result =await _balancersRepository.InsertBalancerEntity(temp);
            return result;
        }

        public async Task<bool> UpdateBalancer(Guid balancerId,BalancerRequest balancer)
        {
            var resultBalancer = await _balancersRepository.GetBalancerById(balancerId);
            if (resultBalancer == null) return false;

            var temp = new Balancer() { Id = balancer.Id, Name = balancer.Name,Ip=balancer.Ip,Ports=balancer.Ports, Active = balancer.Active };
            var result =await _balancersRepository.UpdateBalancerEntity(temp);
            return result;
        }
    }
}
