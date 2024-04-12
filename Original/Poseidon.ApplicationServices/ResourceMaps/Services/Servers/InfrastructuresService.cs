using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using Poseidon.Repositories.ResourceMaps.Interfaces.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Servers
{
    public class InfrastructuresService : IInfrastructuresService
    {
        private readonly IServersRepository _serversRepository;

        public InfrastructuresService(IServersRepository serversRepository)
        {
            _serversRepository = serversRepository;
        }


        public async Task<bool> DeleteInfrastructure(Guid infrastructureId)
        {
            var result = await _serversRepository.DeleteInfrastructureEntity(infrastructureId);
            return result;
        }

        public async Task<Infrastructure> GetInfrastructureById(Guid infrastructureId)
        {
            var result = await _serversRepository.GetInfrastructureById(infrastructureId);
            return result;
        }

        public async Task<List<Infrastructure>> GetInfrastructures()
        {
            var result = await _serversRepository.GetAllInfrastructures();
            return result;
        }

        public async Task<Guid> InsertInfrastructure(InfrastructureRequest infrastructure)
        {
            var temp = new Infrastructure() { Name = infrastructure.Name, Active = infrastructure.Active };
            var result = await _serversRepository.InsertInfrastructureEntity(temp);
            return result;
        }

        public async Task<bool> UpdateInfrastructure(Guid infrastructureId, InfrastructureRequest infrastructure)
        {
            var temp = new Infrastructure() { Id = infrastructureId, Name = infrastructure.Name, Active = infrastructure.Active };
            var result = await _serversRepository.UpdateInfrastructureEntity(temp);
            return result;
        }
    }
}
