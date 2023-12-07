using Poseidon.Api.Models.ResourceMaps.Requests.Servers;
using Poseidon.Entities.ResourceMaps.Servers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Servers
{
    public interface IServersService
    {
        public Task<List<Server>> GetServers();
        public Task<Server> GetServerById(Guid serverId);
        public Task<List<Role>> GetAllRolesByServerId(Guid serverId);
        public Task<Role> GetRoleByServerId(Guid serverId, Guid roleId);
        public Task<List<ServerApplication>> GetAllServerApplicationsByRoleId(Guid serverId, Guid roleId);
        public Task<ServerApplication> GetServerApplicationByRoleId(Guid serverId, Guid roleId, Guid serverApplicationId);
        public Task<Guid> InsertServer(ServerRequest server);
        public Task<bool> DeleteServer(Guid serverId);
        public Task<bool> UpdateServer(Guid serverId,ServerRequest server);
    }
}
