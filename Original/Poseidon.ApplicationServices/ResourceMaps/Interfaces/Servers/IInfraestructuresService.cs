using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers
{
    public interface IInfraestructuresService
    {
        public Task<List<Infraestructure>> GetInfraestructures();
        public Task<Infraestructure> GetInfraestructureById(Guid InfraestructureId);
        public Task<Guid> InsertInfraestructure(InfraestructureRequest infraestructure);
        public Task<bool> DeleteInfraestructure(Guid InfraestructureId);
        public Task<bool> UpdateInfraestructure(Guid InfraestructureId, InfraestructureRequest infraestructure);
    }
}
