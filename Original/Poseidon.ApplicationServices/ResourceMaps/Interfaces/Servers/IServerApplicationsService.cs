using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers
{
    public interface IServerApplicationsService
    {
        public Task<List<ServerApplication>> GetAllServerApplications();
        public Task<ServerApplication> GetServerApplicationById(Guid serverApplicationId);
        public Task<Guid> InsertServerApplication(Guid serverId, Guid roleId, ServerApplicationRequest serverApplication);
        public Task<bool> DeleteServerApplication(Guid serverId, Guid roleId, Guid serverApplicationId);
        public Task<bool> UpdateServerApplication(Guid serverId, Guid roleId, Guid serverApplicationId,ServerApplicationRequest serverApplication);
    }
}
