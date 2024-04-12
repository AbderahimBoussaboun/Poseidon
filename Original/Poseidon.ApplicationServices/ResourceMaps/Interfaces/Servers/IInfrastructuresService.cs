using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers
{
    public interface IInfrastructuresService
    {
        Task<List<Infrastructure>> GetInfrastructures();
        Task<Infrastructure> GetInfrastructureById(Guid infrastructureId);
        Task<Guid> InsertInfrastructure(InfrastructureRequest infrastructure);
        Task<bool> DeleteInfrastructure(Guid infrastructureId);
        Task<bool> UpdateInfrastructure(Guid infrastructureId, InfrastructureRequest infrastructure);
    }
}
