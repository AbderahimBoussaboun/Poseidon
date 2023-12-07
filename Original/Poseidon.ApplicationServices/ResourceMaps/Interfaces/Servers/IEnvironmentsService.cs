
using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers
{
    public interface IEnvironmentsService
    {

        public Task<List<Entities.ResourceMaps.Servers.Environment>> GetEnvironments();
        public Task<Entities.ResourceMaps.Servers.Environment> GetEnvironmentById(Guid id);
        public Task<Guid> InsertEnvironment(EnvironmentRequest environment);
        public Task<bool> DeleteEnvironment(Guid id);
        public Task<bool> UpdateEnvironment(EnvironmentRequest environment);

    }
}
