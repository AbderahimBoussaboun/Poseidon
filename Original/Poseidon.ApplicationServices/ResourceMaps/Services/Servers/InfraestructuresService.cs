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
    public class InfraestructuresService : IInfraestructuresService
    {
        private readonly IServersRepository _serversRepository;

        public InfraestructuresService(IServersRepository serversRepository)
        {
            _serversRepository = serversRepository;
        }


        public async Task<bool> DeleteInfraestructure(Guid InfraestructureId)
        {
            var result = await _serversRepository.DeleteInfraestructureEntity(InfraestructureId);
            return result;
        }

        public async Task<Infraestructure> GetInfraestructureById(Guid InfraestructureId)
        {
            var result = await _serversRepository.GetInfraestructureById(InfraestructureId);
            return result;
        }

        public async Task<List<Infraestructure>> GetInfraestructures()
        {
            var result = await _serversRepository.GetAllInfraestructures();
            return result;
        }

        public async Task<Guid> InsertInfraestructure(InfraestructureRequest infraestructure)
        {
            var temp = new Infraestructure() { Name = infraestructure.Name, Active = infraestructure.Active };
            var result = await _serversRepository.InsertInfraestructureEntity(temp);
            return result;
        }

        public async Task<bool> UpdateInfraestructure(Guid InfraestructureId, InfraestructureRequest infraestructure)
        {
            var temp = new Infraestructure() { Id=InfraestructureId,Name = infraestructure.Name,Active=infraestructure.Active };
            var result = await _serversRepository.UpdateInfraestructureEntity(temp);
            return result;
        }
    }
}
